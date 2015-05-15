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
            
          
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
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
                MessageBox.Show(ex.Message);
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
                timer.Interval = 1000 * 60 * option.ServerIntelval;
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
                    MessageBox.Show(this, e.UserState.ToString());
                    EventLog.WriteEntry("VehicleCheck", e.UserState.ToString());
                    worker.CancelAsync();
                    return;
                }

                Label lblVehicle = new Label();
                lblVehicle.Height = this.Height / 10;
                lblVehicle.Dock = DockStyle.Top;
                lblVehicle.TextAlign = ContentAlignment.MiddleCenter;
                lblVehicle.AutoSize = false;
                lblVehicle.Text = e.UserState.ToString();
                if (lblVehicle.Text.Trim() == "-")
                    return;
                lblVehicle.ForeColor = Color.FromArgb(option.NormalColor);
                if (option.FontFamily.Trim() != string.Empty)
                    lblVehicle.Font = new FontConverter().ConvertFromString(option.FontFamily) as Font;
                if (CheckBlackList(e.UserState.ToString()))
                {
                    lblVehicle.ForeColor = Color.FromArgb(option.AlarmColor);
                    player.Play();
                }

                if (this.Controls.Count >= 10)
                    this.Controls.RemoveAt(0);
                this.Controls.Add(lblVehicle);
                lblVehicle.BringToFront();
                GC.Collect(0);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("VehicleCheck", ex.Message);
            }
        }

        private bool CheckBlackList(string vehNo)
        {
            if (vehNo.StartsWith("新"))
            {
                return true;
            }
            string sql = "select count(*) from BlackList where VehNo='{0}'";
            SqlConnection conn = new SqlConnection(connstring);
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format(sql, vehNo));
                cmd.Connection = conn;
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            if (count > 0)
                return true;
            else
                return false;
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
