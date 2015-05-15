using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using VehicleChecking.IMOS;

namespace VehicleChecking
{
    
    public class IMOSHelper
    {

        string connstring = System.Configuration.ConfigurationManager.AppSettings["conn"];

        private SettingOption setting;
        /// <summary>
        /// IMOS回调句柄
        /// </summary>
        public static IMOSSDK.XP_RUN_INFO_CALLBACK_EX_PF callBackMsg;
        /// <summary>
        /// SDK Manager
        /// </summary>
        public SdkManager sdkManager { get; set; }

        //用户登录参数
        public String strSrvIpAddr { get; set; }
        public uint uSrvPort { get; set; }
        public String strUsrLoginName { get; set; }
        public String strUsrLoginPsw { get; set; }
        

        //登录成功返回信息
        public LOGIN_INFO_S stLoginInfo;

        public IMOSHelper()
        {
            setting = SettingOption.Load();
        }

        public void Init()
        {
            //设置回调函数
            callBackMsg = XpInfoRush;
            //获取回调句柄
            IntPtr ptrCallbakc = Marshal.GetFunctionPointerForDelegate(callBackMsg);
            //初始化操作
            uint ulResult = IMOSSDK.IMOS_SetRunMsgCB(ptrCallbakc);

            if (0 != ulResult)
            {
                //Log.Write.Error("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
                //MessageBox.Show("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
            }

            IMOS_Login();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public bool IMOS_Login()
        {
            try
            {
                //登陆
                uint ulRet = 0;

                this.strUsrLoginName = setting.ServerUsername;
                this.strUsrLoginPsw = setting.ServerPassword;
                this.strSrvIpAddr = setting.ServerAddress;
                this.uSrvPort = (uint)setting.ServerPort;

                sdkManager = new SdkManager();
                //执行登陆
                stLoginInfo = sdkManager.LoginMethod(strUsrLoginName, strUsrLoginPsw, strSrvIpAddr, uSrvPort, "N/A");

                this.sdkManager.stLoginInfo = stLoginInfo;
                if (0 != ulRet)
                {
                    //MessageBox.Show("登录失败！" + ulRet.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public bool IMOS_Logout()
        {
            //1.注销登录
            if (null != stLoginInfo.stUserLoginIDInfo.szUserLoginCode)
            {
                IMOSSDK.IMOS_LogoutEx(ref stLoginInfo.stUserLoginIDInfo);
                //IMOSSDK.IMOS_CleanUp()
                return true;
            }
            else
            {
                MessageBox.Show("你还没有登录!");
                return false;
            }
        }

        /// <summary>
        /// XP回调处理函数
        /// </summary>
        /// <param name="stUserLoginIDInfo"></param>
        /// <param name="ulRunInfoType"></param>
        /// <param name="ptrInfo"></param>
        public void XpInfoRush(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, uint ulRunInfoType, IntPtr ptrInfo)
        {
            //不需要
            //if (ulRunInfoType == (uint)XP_RUN_INFO_TYPE_E.XP_RUN_INFO_DOWN_RTSP_PROTOCOL)
            //{
            //    //下载rtsp消息
            //    DownLoadComplete(ptrInfo);
            //}
        }

        public void GetBlackList()
        {
            UTF8Encoding encoder = new UTF8Encoding();
            List<VEHICLE_BLACKLIST_S> list = sdkManager.getBlacklist();
            string sqlClear = "DELETE * FROM BlackList";
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlClear, conn, tran);
                cmd.ExecuteNonQuery();
                
                foreach (VEHICLE_BLACKLIST_S vehInfo in list)
                {
                    cmd.CommandText = "insert into blacklist(vehno) values('" + encoder.GetString(vehInfo.szLicensePlate) + "')";
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
