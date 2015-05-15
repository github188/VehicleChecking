using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;

namespace VehicleChecking.IMOS
{
    public class SdkManager
    {
        public const uint EX_SUCCESS = 0;
        public const uint EX_FAILED = 0;
        public const string ALARMSEQ = "告警序号";
        public const string ALARMEVENTCODE = "告警事件编码";
        public const string ALARMSRCCODE = "告警源编码";
        public const string ALRAMSRCNAME = "告警源名称";
        public const string ACTIVENAME = "使能后名称";
        public const string ALARMTYPE = "告警类型";
        public const string ALARMLEVEL = "告警级别";
        public const string ALARMTIME = "告警触发时间";
        public const string ALARMDESC = "告警描述";

        private string m_strDateFormat = "yyyy-MM-dd HH:mm:ss";
        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

        //登录成功返回信息
        public LOGIN_INFO_S stLoginInfo;

        //用户登录参数
        public String srvIpAddr { get; set; }
        public String usrLoginName { get; set; }
        public String usrLoginPsw { get; set; }

        //回调函数指针
        public IMOSSDK.CALL_BACK_PROC_PF CallBackFunc;


        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        public LOGIN_INFO_S LoginMethod(String usrLoginName, String usrLoginPsw, String srvIpAddr,uint srvPort, String cltIpAddr)
        {
             
            string message = string.Empty;
               
            UInt32 ulRet = 0;              

            //1.初始化
            ulRet = IMOSSDK.IMOS_Initiate("N/A", srvPort, 1, 1);
            if (0 != ulRet)
            {
                throw new Exception("初始化失败!" + ulRet.ToString());
            }

            //2.加密密码
            IntPtr ptr_MD_Pwd = Marshal.AllocHGlobal(sizeof(char) * IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN);
            ulRet = IMOSSDK.IMOS_Encrypt(usrLoginPsw, (UInt32)usrLoginPsw.Length, ptr_MD_Pwd);

            if (0 != ulRet)
            {
                throw new Exception("加密密码失败!" + ulRet.ToString());
            }

            String MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
            Marshal.FreeHGlobal(ptr_MD_Pwd);

            //3.登录方法
            IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
            ulRet = IMOSSDK.IMOS_LoginEx(usrLoginName, MD_PWD, srvIpAddr, cltIpAddr, ptrLoginInfo);
            if (0 != ulRet)
            {
                throw new Exception("IMOS_Login" + ulRet.ToString());
            }

            stLoginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
            Marshal.FreeHGlobal(ptrLoginInfo);

            //4.保活

            return stLoginInfo;
        }


        public void getLoginInfo(String srvIpAddr, String usrLoginName, String usrLoginPsw, String cltIpAddr)
        {
            this.srvIpAddr = srvIpAddr;
            this.usrLoginName = usrLoginName;
            this.usrLoginPsw = usrLoginPsw;
        }

        public List<VEHICLE_BLACKLIST_S> getBlacklist()
        {
            IntPtr ptrVehList = IntPtr.Zero;
            IntPtr ptrRspPage = IntPtr.Zero;
            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;

                ptrVehList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VEHICLE_BLACKLIST_S)) * 30);
                ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<VEHICLE_BLACKLIST_S> list = new List<VEHICLE_BLACKLIST_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;
                    ulRet = IMOSSDK.IMOS_QueryVehicleBlacklistList(ref stLoginInfo.stUserLoginIDInfo, 0, ref stPageInfo, ptrRspPage, ptrVehList);
                  
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    VEHICLE_BLACKLIST_S stOrgResItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrVehList.ToInt32() + Marshal.SizeOf(typeof(VEHICLE_BLACKLIST_S)) * i);
                        stOrgResItem = (VEHICLE_BLACKLIST_S)Marshal.PtrToStructure(ptrTemp, typeof(VEHICLE_BLACKLIST_S));
                        list.Add(stOrgResItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定节点下的域数据列表。
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<ORG_RES_QUERY_ITEM_S> GetDomain(TreeNode treeNode)
        {
            IntPtr ptrResList = IntPtr.Zero;
            IntPtr ptrRspPage = IntPtr.Zero;
            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 30);
                ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;

                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stLoginInfo.stUserLoginIDInfo, st.szResCode, (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG, 0, ref stPageInfo, ptrRspPage, ptrResList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    ORG_RES_QUERY_ITEM_S stOrgResItem;
                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt32() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrResList);
                Marshal.FreeHGlobal(ptrRspPage);
            }
        }

        /// <summary>
        /// 获取指定节点下的摄像机数据列表。
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<ORG_RES_QUERY_ITEM_S> GetCamera(TreeNode treeNode)
        {
            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                ORG_RES_QUERY_ITEM_S st = (ORG_RES_QUERY_ITEM_S)treeNode.Tag;

                IntPtr ptrResList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * 30);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));

                RSP_PAGE_INFO_S stRspPageInfo;
                List<ORG_RES_QUERY_ITEM_S> list = new List<ORG_RES_QUERY_ITEM_S>();
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    stPageInfo.bQueryCount = 1;
                    ulRet = IMOSSDK.IMOS_QueryResourceList(ref stLoginInfo.stUserLoginIDInfo, st.szResCode, (UInt32)IMOS_TYPE_E.IMOS_TYPE_CAMERA, 0, ref stPageInfo, ptrRspPage, ptrResList);
                    if (0 != ulRet)
                    {
                        return null;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;
                    ORG_RES_QUERY_ITEM_S stOrgResItem;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrResList.ToInt32() + Marshal.SizeOf(typeof(ORG_RES_QUERY_ITEM_S)) * i);
                        stOrgResItem = (ORG_RES_QUERY_ITEM_S)Marshal.PtrToStructure(ptrTemp, typeof(ORG_RES_QUERY_ITEM_S));
                        list.Add(stOrgResItem);
                    }

                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return list;

            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeNode"></param>
        private void BuildTree(TreeNode treeNode,TreeView treeView)
        {
            try
            {
                treeView.BeginUpdate();
                //获取域数据
                List<ORG_RES_QUERY_ITEM_S> list = GetDomain(treeNode);

                //List<ORG_RES_QUERY_ITEM_S> list = getTestDomain();

                if (null != list)
                {
                    foreach (ORG_RES_QUERY_ITEM_S org in list)
                    {
                        TreeNode domainNode = new TreeNode();
                        domainNode.Text = IMOSSDK.UTF8ToUnicode(org.szResName);
                        domainNode.Tag = org;
                        domainNode.ImageIndex = 0;
                        domainNode.SelectedImageIndex = 0;
                        treeNode.Nodes.Add(domainNode);
                    }
                }
                treeNode.ExpandAll();
                //获取摄像机数据
                List<ORG_RES_QUERY_ITEM_S> cameraList = GetCamera(treeNode);
                //List<ORG_RES_QUERY_ITEM_S> cameraList = getTestCamera();
                if (null == cameraList)
                {
                    return;
                }

                foreach (ORG_RES_QUERY_ITEM_S camera in cameraList)
                {
                    TreeNode cameraNode = new TreeNode();
                    cameraNode.Text = IMOSSDK.UTF8ToUnicode(camera.szResName);
                    cameraNode.Tag = camera;

                    if (2 == camera.ulResSubType || 4 == camera.ulResSubType)
                    {
                        //云台摄像机
                        switch (camera.ulResStatus)
                        {
                            case IMOSSDK.IMOS_DEV_STATUS_ONLINE:
                                cameraNode.ImageIndex = 1;
                                cameraNode.SelectedImageIndex = 1;
                                break;
                            case IMOSSDK.IMOS_DEV_STATUS_OFFLINE:
                                cameraNode.ImageIndex = 2;
                                cameraNode.SelectedImageIndex = 2;
                                break;
                            default:
                                cameraNode.ImageIndex = 2;
                                cameraNode.SelectedImageIndex = 2;
                                break;
                        }
                    }
                    else
                    {
                        switch (camera.ulResStatus)
                        {
                            case IMOSSDK.IMOS_DEV_STATUS_ONLINE:
                                cameraNode.ImageIndex = 4;
                                cameraNode.SelectedImageIndex = 4;
                                break;
                            case IMOSSDK.IMOS_DEV_STATUS_OFFLINE:
                                cameraNode.ImageIndex = 5;
                                cameraNode.SelectedImageIndex = 5;
                                break;
                            default:
                                cameraNode.ImageIndex = 4;
                                cameraNode.SelectedImageIndex = 4;
                                break;
                        }
                    }
 
                    treeNode.Nodes.Add(cameraNode);
                }
            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        /// <summary>
        /// 设置根节点
        /// </summary>
        public void SetTreeRoot(TreeView treeView)
        {
            try
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = IMOSSDK.UTF8ToUnicode(stLoginInfo.szDomainName);
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = new ORG_RES_QUERY_ITEM_S();
                stOrgQueryItem.szOrgCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.szResName = stLoginInfo.szDomainName;
                stOrgQueryItem.szResCode = stLoginInfo.szOrgCode;
                stOrgQueryItem.ulResType = (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG;
                treeNode.Tag = stOrgQueryItem;
                treeView.Nodes.Add(treeNode);
                treeView.ExpandAll();
    
            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
            }
        }

        /// <summary>
        /// 根据当前节点，显示该节点下的子节点。
        /// </summary>
        /// <param name="parentNode"></param>
        public void OrganizeChildrenNodes(TreeNode parentNode, TreeView treeView)
        {
            if (null == parentNode)
            {
                return;
            }
            try
            {
                ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;
                //ORG_RES_QUERY_ITEM_S stOrgQueryItem = (ORG_RES_QUERY_ITEM_S)parentNode.Tag;

                if (stOrgQueryItem.ulResType == (UInt32)IMOS_TYPE_E.IMOS_TYPE_ORG)
                {
                    parentNode.Nodes.Clear();
                    BuildTree(parentNode, treeView);
                }
            }
            catch (Exception ex)
            {
                //log.Info(ex.StackTrace);
                MessageBox.Show("获取子节点失败，详情请查询日志信息。");
            }

        }

 



        public List<RECORD_FILE_INFO_S> queryRecord(string strBegin, string strEnd, String m_cameraCode)
        {   
            List<RECORD_FILE_INFO_S> RecFileList = new List<RECORD_FILE_INFO_S>();

            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                RSP_PAGE_INFO_S stRspPageInfo;
                

                REC_QUERY_INFO_S stRecQueryInfo = new REC_QUERY_INFO_S();
                stRecQueryInfo.szReserve = new byte[32];
                //stRecQueryInfo.stQueryTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                stRecQueryInfo.szCamCode = Encoding.Default.GetBytes(m_cameraCode);
                stRecQueryInfo.stQueryTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strBegin, 0, Encoding.Default.GetByteCount(strBegin), stRecQueryInfo.stQueryTimeSlice.szBeginTime, 0);
                stRecQueryInfo.stQueryTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strEnd, 0, Encoding.Default.GetByteCount(strEnd), stRecQueryInfo.stQueryTimeSlice.szEndTime, 0);

                //stRecQueryInfo.stQueryTimeSlice.szBeginTime =  Encoding.Default.GetBytes(strBegin);
                //stRecQueryInfo.stQueryTimeSlice.szEndTime = Encoding.Default.GetBytes(strEnd);

                IntPtr ptrRecFileList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * 30);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    ulRet = IMOSSDK.IMOS_RecordRetrieval(ref stLoginInfo.stUserLoginIDInfo, ref stRecQueryInfo, ref stPageInfo, ptrRspPage, ptrRecFileList);
                    if (0 != ulRet)
                    {
                        return RecFileList;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    RECORD_FILE_INFO_S stRecFileItem;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrRecFileList.ToInt32() + Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * i);
                        stRecFileItem = (RECORD_FILE_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(RECORD_FILE_INFO_S));
                        RecFileList.Add(stRecFileItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return RecFileList;
            }
            catch (System.Exception ex)
            {
                return RecFileList;
            }
        }
  
      
        public void SpeedPlay(byte[] szChannelCode, uint playSpeed)
        {
            IMOSSDK.IMOS_SetPlaySpeed(ref stLoginInfo.stUserLoginIDInfo, szChannelCode, playSpeed);
        }

        /// <summary>
        /// 设置预置位
        /// </summary>
        /// <param name="szCamCode"></param>
        /// <param name="preset"></param>
        /// <returns></returns>
        public UInt32 setPresetLoc(byte[] szCamCode, PRESET_INFO_S preset)
        {
            UInt32 ulRet = 0;
            ulRet = IMOSSDK.IMOS_SetPreset(ref stLoginInfo.stUserLoginIDInfo, szCamCode, ref preset);
            return ulRet;
        }

        /// <summary>
        /// 使用预置位
        /// </summary>
        /// <param name="szCamCode"></param>
        /// <param name="ulPresetNum"></param>
        /// <returns></returns>
        public UInt32 usePreset(byte[] szCamCode, uint ulPresetNum)
        {
            UInt32 ulRet = 0;
            ulRet = IMOSSDK.IMOS_UsePreset(ref stLoginInfo.stUserLoginIDInfo, szCamCode, ulPresetNum);
            return ulRet;
        }

        /// <summary>
        /// 回放下载
        /// </summary>
        /// <param name="recFile">录像信息</param>
        /// <param name="camCode">摄像机编码</param>
        /// <param name="downloadLoc">用户下载存储位置</param>
        /// <returns></returns>
        public byte[] startDownLoad(RECORD_FILE_INFO_S fileInfo, byte[] camCode, XP_PROTOCOL_E vodProtocol,String downloadLoc, XP_DOWN_MEDIA_SPEED_E speed, uint downloadFormat, DateTime beginTime, DateTime endTime)
        {
            UInt32 ulRet = 0;
            IntPtr ptrSDKURLInfo = IntPtr.Zero;
            //IntPtr pcChannelCode = IntPtr.Zero;

            try
            {
                GET_URL_INFO_S getUrlInfo = new GET_URL_INFO_S();
                TIME_SLICE_S timeSlice = new TIME_SLICE_S();
                URL_INFO_S urlInfo = new URL_INFO_S();

                byte[] begin = new byte[IMOSSDK.IMOS_TIME_LEN];
                String strBeginTime = beginTime.ToString("yyyy-MM-dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strBeginTime).CopyTo(begin, 0);
                byte[] end = new byte[IMOSSDK.IMOS_TIME_LEN];
                String strEndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strEndTime).CopyTo(end, 0);
   
                timeSlice.szBeginTime = begin;
                timeSlice.szEndTime = end;

                getUrlInfo.szCamCode = camCode;
                getUrlInfo.szFileName = fileInfo.szFileName;
                getUrlInfo.stRecTimeSlice = timeSlice;
                getUrlInfo.szClientIp = stLoginInfo.stUserLoginIDInfo.szUserIpAddress;

                //ptrSDKURLInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(URL_INFO_S)));
                //这个下载通道号，是录像下载的唯一标志，以后查询录像都要用到这个通道号
                byte[] byPcChannel = new byte[IMOSSDK.IMOS_RES_CODE_LEN];

                ulRet = IMOSSDK.IMOS_GetRecordFileURL(ref stLoginInfo.stUserLoginIDInfo, ref getUrlInfo, ref urlInfo);

                //URL_INFO_S URLInfo = (URL_INFO_S)Marshal.PtrToStructure(ptrSDKURLInfo, typeof(URL_INFO_S));

                //byte[] pcFileName = Encoding.UTF8.GetBytes(downloadLoc);
                ulRet = IMOSSDK.IMOS_OpenDownload(ref stLoginInfo.stUserLoginIDInfo,
                    urlInfo.szURL, urlInfo.stVodSeverIP.szServerIp, urlInfo.stVodSeverIP.usServerPort,
                    (uint)vodProtocol, (uint)speed,
                    Encoding.Default.GetBytes(downloadLoc), downloadFormat, byPcChannel);

                ulRet = IMOSSDK.IMOS_SetDecoderTag(ref stLoginInfo.stUserLoginIDInfo, byPcChannel, urlInfo.szDecoderTag);

                ulRet = IMOSSDK.IMOS_StartDownload(ref stLoginInfo.stUserLoginIDInfo, byPcChannel);

                return byPcChannel;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrSDKURLInfo);
            }
        }
    
        /// <summary>
        /// 获取当前下载时间
        /// </summary>
        /// <param name="channelCode">下载通道号</param>
        /// <returns></returns>
        public DateTime getCurrDownLoadTime(byte[] channelCode)
        {
            dtFormat.ShortTimePattern = "yyyy-MM-dd HH:mm:ss";
                DateTime dt = new DateTime();
                UInt32 ulRet = 0;

                byte[] currTime = new byte[IMOSSDK.IMOS_TIME_LEN];   //当前下载时间;
                ulRet = IMOSSDK.IMOS_GetDownloadTime(ref stLoginInfo.stUserLoginIDInfo, Encoding.UTF8.GetString(channelCode), currTime);
                //curr = getTotalTime(currTime);

                String strTime = Encoding.UTF8.GetString(currTime);

                if (!String.IsNullOrEmpty(strTime.TrimEnd('\0')))
                {                  
                    dt = Convert.ToDateTime(strTime, dtFormat);
                }
                return dt;
        }

        public void SaveAlarmInfo(IntPtr ptrParam)
        {

        }
    }
}
