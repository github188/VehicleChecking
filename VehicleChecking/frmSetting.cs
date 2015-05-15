using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VehicleChecking
{
    public partial class frmSetting : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        SettingOption option;
        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            option = SettingOption.Load();
            txtLeftMargin.Text = option.LeftMargin.ToString();
            txtTopMargin.Text = option.TopMargin.ToString();
            txtWidth.Text = option.Width.ToString();
            txtHeight.Text = option.Height.ToString();
            lblNormal.BackColor = Color.FromArgb(option.NormalColor);
            lblBackground.BackColor = Color.FromArgb(option.BackgroundColor);
            lblAlarm.BackColor = Color.FromArgb(option.AlarmColor);
            lblFontFamily.Text = option.FontFamily;
            if (lblFontFamily.Text.Trim() != string.Empty)
                lblFontFamily.Font = new FontConverter().ConvertFromString(option.FontFamily) as Font;

            txtLocalIp.Text = option.LocalIp;
            txtLocalPort.Text = option.LocalPort.ToString();
            txtServerAddress.Text = option.ServerAddress;
            txtServerPort.Text = option.ServerPort.ToString();
            txtUserName.Text = option.ServerUsername;
            txtPassword.Text = option.ServerPassword;
            txtServerInterval.Text = option.ServerIntelval.ToString();

            txtSoundFile.Text = option.SoundPath;
        }

        private void lblNormal_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            dlgColor.AllowFullOpen = true;
            dlgColor.AnyColor = true;
            Label lblColor = sender as Label;
            dlgColor.Color = lblColor.BackColor;
            if (dlgColor.ShowDialog(this) == DialogResult.OK)
            {
                lblColor.BackColor = dlgColor.Color;
            }
        }

        private void lblFontFamily_Click(object sender, EventArgs e)
        {
            FontDialog dlgFont = new FontDialog();
            dlgFont.Font = lblFontFamily.Font;
            if (dlgFont.ShowDialog(this) == DialogResult.OK)
            {
                lblFontFamily.Font = dlgFont.Font;
                lblFontFamily.Text = dlgFont.Font.ToString();

            }
        }

        private void btnChoiceSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgFile = new OpenFileDialog();
            dlgFile.Filter = "声音文件|*.wav";
            if (dlgFile.ShowDialog(this) == DialogResult.OK)
            {
                txtSoundFile.Text = dlgFile.FileName;
            }
        }

        private void btnPlaySound_Click(object sender, EventArgs e)
        {
            if (btnPlaySound.Text == "播放")
            {
                if (txtSoundFile.Text.Trim() != string.Empty)
                {
                    player.SoundLocation = txtSoundFile.Text.Trim();
                    player.Play();
                    btnPlaySound.Text = "停止";
                }
            }
            else
            {
                player.Stop();
                btnPlaySound.Text = "播放";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int leftmargin = 0;
            if (int.TryParse(txtLeftMargin.Text, out leftmargin))
            {
                option.LeftMargin = leftmargin;
            }
            else
            {
                MessageBox.Show(this, "LED显示左边距格式不正确，请重新输入");
                return;
            }
            int topMargin = 0;
            if (int.TryParse(txtTopMargin.Text, out topMargin))
            {
                option.TopMargin = topMargin;
            }
            else
            {
                MessageBox.Show(this, "LED显示顶边距格式不正确，请重新输入");
                return;
            }
            int height = 0;
            if (int.TryParse(txtHeight.Text, out height))
            {
                option.Height = height;
            }
            else
            {
                MessageBox.Show(this, "LED显示高度格式不正确，请重新输入");
                return;
            }
            int width = 0;
            if (int.TryParse(txtWidth.Text, out width))
            {
                option.Width = width;
            }
            else
            {
                MessageBox.Show(this, "LED显示宽度格式不正确，请重新输入");
                return;
            }
            
            option.NormalColor = lblNormal.BackColor.ToArgb();
            option.AlarmColor = lblAlarm.BackColor.ToArgb();
            option.BackgroundColor = lblBackground.BackColor.ToArgb();

            option.FontFamily = new FontConverter().ConvertToString(lblFontFamily.Font);

            IPAddress localIp = null;
            if (IPAddress.TryParse(txtLocalIp.Text, out localIp))
            {
                option.LocalIp = txtLocalIp.Text;
            }
            else
            {
                MessageBox.Show(this, "过车信息本机地址格式不正确，请重新输入");
                return;
            }

            int localPort = 0;
            if (int.TryParse(txtLocalPort.Text, out localPort))
            {
                option.LocalPort = localPort;
            }
            else
            {
                MessageBox.Show(this, "L过车信息本机端口格式不正确，请重新输入");
                return;
            }

            IPAddress serverIp = null;
            if (IPAddress.TryParse(txtServerAddress.Text, out serverIp))
            {
                option.ServerAddress = txtServerAddress.Text;
            }
            else
            {
                MessageBox.Show(this, "黑名单服务器地址格式不正确，请重新输入");
                return;
            }

            int serverPort = 0;
            if (int.TryParse(txtServerPort.Text, out serverPort))
            {
                option.ServerPort = serverPort;
            }
            else
            {
                MessageBox.Show(this, "黑名单服务器端口格式不正确，请重新输入");
                return;
            }

            int serverInterval = 0;
            if (int.TryParse(txtServerInterval.Text, out serverInterval))
            {
                option.ServerIntelval = serverInterval;
            }
            else
            {
                MessageBox.Show(this, "黑名单服务器获取时间间隔格式不正确，请重新输入");
                return;
            }

            option.ServerUsername = txtUserName.Text;
            option.ServerPassword = txtPassword.Text;
            option.SoundPath = txtSoundFile.Text;
            option.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}
