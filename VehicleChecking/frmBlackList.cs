using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VehicleChecking
{
    public partial class frmBlackList : Form
    {
        string connstring = System.Configuration.ConfigurationManager.AppSettings["conn"];
        public frmBlackList()
        {
            InitializeComponent();
        }

        private void frmBlackList_Load(object sender, EventArgs e)
        {
            string sql = "select * from BlackList";
            SqlConnection conn = new SqlConnection(connstring);
            int count = 0;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conn);
                
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                grdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
