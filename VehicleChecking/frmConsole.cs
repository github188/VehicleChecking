using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace VehicleChecking
{
    public partial class frmConsole : Form
    {
        private frmLED ledView;
        public frmConsole()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            
            if (ledView!=null)
            {
                ledView.Close();
                ledView.Dispose();
                ledView = null;
                btn.Text = "启动";
            }
            else
            {
                ledView = new frmLED();
                ledView.Show(this);
                btn.Text = "停止";
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSetting settingForm = new frmSetting();
            if (settingForm.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show(this, "设置成功，请重新启动显示界面！");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmBlackList frm = new frmBlackList();
            frm.ShowDialog(this);
        }
    }
}
