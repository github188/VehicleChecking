using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VehicleChecking
{
    public partial class frmLED : Form
    {
        BackgroundWorker worker = new BackgroundWorker();
        string connstring = System.Configuration.ConfigurationManager.AppSettings["conn"];
        TcpServer server;
        SettingOption option;
        System.Media.SoundPlayer player;
        System.Timers.Timer timer = new System.Timers.Timer();
        bool onGet = false;
        List<Label> lblList = new List<Label>();

        public frmLED()
        {
            InitializeComponent();
            option = SettingOption.Load();
            timer.Elapsed += timer_Elapsed;
            player = new System.Media.SoundPlayer();
            player.SoundLocation = option.SoundPath;

            this.StartPosition = FormStartPosition.Manual;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Left = option.LeftMargin;
            this.Top = option.TopMargin;
            this.Width = option.Width;
            this.Height = option.Height;
            this.BackColor = Color.FromArgb(option.BackgroundColor);
            this.TopMost = true;
          
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (onGet)
                return;
            try
            {
                if (!onGet)
                {
                    onGet = true;
                    IMOSHelper helper = new IMOSHelper();
                    if (helper.IMOS_Login())
                    {
                        helper.GetBlackList();
                        helper.IMOS_Logout();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                onGet = false;
            }
            finally
            {
                onGet = false;
            }
        }



        private void frmLED_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = 1000 *60 * option.ServerIntelval;
                timer.Enabled = true;
                timer.Start();

                server = new TcpServer();
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("VehicleCheck", ex.Message);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == 99)
                {
                    EventLog.WriteEntry("VehicleCheck", e.UserState.ToString());
                    worker.CancelAsync();
                    return;
                }

                Vehinfo info = e.UserState as Vehinfo;

                if (info == null || !info.IsMatched)
                    return;

                Label lblVehicle = new Label();
                lblVehicle.Height = this.Height / 8;
                lblVehicle.Dock = DockStyle.Top;
                lblVehicle.TextAlign = ContentAlignment.MiddleCenter;
                lblVehicle.AutoSize = false;
                lblVehicle.Text = info.CarPlate;
                lblVehicle.ForeColor = Color.FromArgb(option.NormalColor);
                if (option.FontFamily.Trim() != string.Empty)
                    lblVehicle.Font = new FontConverter().ConvertFromString(option.FontFamily) as Font;

                if (lblVehicle.Text.Trim() == "-")//去除无效车辆
                    return;

                switch (info.PlateColor)
                {
                    case ConstDefine.PlateColorBlack:
                        lblVehicle.BackColor = Color.Black;
                        break;
                    case ConstDefine.PlateColorBlue:
                        lblVehicle.BackColor = Color.Blue;
                        break;
                    case ConstDefine.PlateColorGreen:
                        lblVehicle.BackColor = Color.Green;
                        break;
                    case ConstDefine.PlateColorYellow:
                        lblVehicle.BackColor = Color.Yellow;
                        break;
                    case ConstDefine.PlateColorWhite:
                        lblVehicle.BackColor = Color.White;
                        break;
                    case ConstDefine.PlateColorOther:
                    default:
                        lblVehicle.BackColor = Color.Black;
                        break;
                }

                if (CheckBlackList(info.CarPlate))
                {
                    lblVehicle.ForeColor = Color.FromArgb(option.AlarmColor);
                    try
                    {
                        player.Play();
                    }
                    catch { }
                }

                lblList.Add(lblVehicle);

                if (lblList.Count>8)
                {
                    this.Controls.Remove(lblList[0]);
                    lblList.Remove(lblList[0]);
                }
                this.Controls.Add(lblVehicle);
                lblVehicle.BringToFront();
           
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("VehicleCheck", ex.Message);
            }
        }

        private bool CheckBlackList(string vehNo)
        {

            if (vehNo.Trim() == string.Empty)
                return false;

            if (vehNo.StartsWith("新"))
            {
                return true;
            }

            if (Program.BlackList.Contains(vehNo))
                return true;
            else
            {
                foreach (string vehCheck in Program.BlackList)
                {
                    if (vehCheck.Trim() == string.Empty)
                        continue;
                    if (vehNo.Contains(vehCheck))
                        return true;
                }
                return false;
            }

            #region old code for db read
            //string sql = "select count(*) from BlackList where VehNo='{0}'";
            //SqlConnection conn = new SqlConnection(connstring);
            //int count = 0;
            //try
            //{
            //    SqlCommand cmd = new SqlCommand(string.Format(sql, "冀GUC708"));
            //    cmd.Connection = conn;
            //    conn.Open();
            //    count = (int)cmd.ExecuteScalar();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}
            //if (count > 0)
            //    return true;
            //else
            //    return false;
            #endregion
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    if (Program.VehicleCheckingQueue.Count > 0)
                    {
                        worker.ReportProgress(0, Program.VehicleCheckingQueue.Dequeue());
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(99, ex.Message);
            }
            finally
            {
            }

        }

    }
}
