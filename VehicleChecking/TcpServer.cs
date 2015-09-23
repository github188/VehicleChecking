using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace VehicleChecking
{
    
    public class TcpServer
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private bool isStart = true;
        private SettingOption option;
        public TcpServer()
        {
            option = SettingOption.Load();
            this.tcpListener = new TcpListener(IPAddress.Parse(option.LocalIp), option.LocalPort);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.IsBackground = true;
            this.listenThread.Start();
        }
        public void Dispose()
        {
            isStart = false;
            this.tcpListener.Stop();
            this.listenThread.Abort();
            this.listenThread.Join();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (isStart)
            {
                //blocks until a client has connected to the server
                try
                {
                    TcpClient client = this.tcpListener.AcceptTcpClient();

                    //create a thread to handle communication 
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
                catch { }
                System.Diagnostics.Debug.WriteLine("tcplistener.stop111");
            }
            this.tcpListener.Stop();
            System.Diagnostics.Debug.WriteLine("tcplistener.stop");
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;
            UInt32 packLength=0;
            byte[] packData = new byte[0];
            long packpos=0;

           // MemoryStream ms = new MemoryStream();
            while (isStart)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }
                
                //message has successfully been received
                //UTF8Encoding encoder = new UTF8Encoding();
                byte[] data = new byte[16];
                Buffer.BlockCopy(message, 0, data, 0, 16);
                
                //BitConverter.ToUInt32(data,4);

                //for (int i = 0; i < 16; i++)
                //{
                //    System.Diagnostics.Debug.WriteLine(Convert.ToUInt32(data[i]));
                //}
                //System.Diagnostics.Debug.WriteLine(BitConverter.ToUInt32(data, 8));
                //System.Diagnostics.Debug.WriteLine(TcpServer.ReverseBytes( BitConverter.ToUInt32(data, 8)));

                UTF8Encoding encoder = new UTF8Encoding();

                UInt32 header = 0x77aa77aa;
                byte[] messageHeader = new byte[4];//包头
                byte[] bVersion = new byte[4];//版本号
                byte[] bCommand = new byte[4];//命令码
                byte[] bXmlLen = new byte[4];//xml长度

                //Buffer.BlockCopy(message, 0, messageHeader, 0, 4);
                if (header - TcpServer.ReverseBytes(BitConverter.ToUInt32(message, 0)) == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("Get Header ======================");
                    //识别到头文件
                    packpos = 0;
                    packLength = TcpServer.ReverseBytes(BitConverter.ToUInt32(message, 4));
                    long version = TcpServer.ReverseBytes(BitConverter.ToUInt32(message, 8));
                    long command = TcpServer.ReverseBytes(BitConverter.ToUInt32(message, 12));
                    System.Diagnostics.Debug.WriteLine(command);
                    //if (command == 111 || command == 211 || command == 212 || command == 213 || command == 214)
                    if (command == 213)
                    {
                        long xmlLen = TcpServer.ReverseBytes(BitConverter.ToUInt32(message, 16));
                        byte[] xmlData = new byte[xmlLen];
                        int xmlPos = 0;
                        do
                        {
                            int readlen = xmlLen > message.Length - 20 ? message.Length - 20 : (int)xmlLen - xmlPos;
                            Buffer.BlockCopy(message, 20, xmlData, xmlPos, (int)readlen);
                            xmlPos = readlen;
                            if (readlen < xmlLen)
                            {
                                
                                clientStream.Read(message, 0, 4096);
                            }
                        } while (xmlPos < xmlLen - 1);
                        //转换字符串并去除结束标记
                        string vehxml = encoder.GetString(xmlData).Replace("\0", "").Replace("\r\n","");
                        //搜索字符串

                        Vehinfo vehInfo = new Vehinfo(vehxml);

                        //Regex reg = new Regex(@"<CarPlate\>(.*?)\<\/CarPlate\>");
                        //Match match = reg.Match(vehxml);
                        //if (match.Success)
                        //{
                        //    string vehNo = match.Value.Replace(@"<CarPlate>", "").Replace(@"</CarPlate>", "");
                        //    if (vehNo.Trim() != string.Empty)
                        //    {
                        //        Program.VehicleCheckingQueue.Enqueue(vehNo.Trim());
                        //        //System.Diagnostics.Debug.WriteLine(vehNo.Trim());
                        //    }
                        //} 
                        if (vehInfo.IsMatched)
                        {
                            Program.VehicleCheckingQueue.Enqueue(vehInfo);
                        }                       
                    }

                   // packData = new byte[packLength];
                   // ms = new MemoryStream();
                   //// System.Diagnostics.Debug.WriteLine("Pack Size ======================" + packLength.ToString());
                   // long readLength = 0;

                   // if (packLength - packpos > message.Length - 20)
                   // {
                   //     readLength = message.Length - 20;
                   // }else{
                   //     readLength = packLength-packpos;
                   // }
                   // //System.Diagnostics.Debug.WriteLine("Read Length ======================" + readLength.ToString());
                   // //System.Diagnostics.Debug.WriteLine("Pack Pos ======================" + packpos.ToString());
                   // //Buffer.BlockCopy(message, 16, packData,(int)packpos, (int)readLength);

                   // ms.Write(message, 20, (int)readLength);
                   // //System.Diagnostics.Debug.WriteLine(encoder.GetString(packData));
                   // //System.Diagnostics.Debug.WriteLine("");
                   // //Array.ForEach<byte>(packData,a=>{
                        
                   // //    System.Diagnostics.Debug.Write(a.ToString()+"-");
                        
                   // //});
                   // System.Diagnostics.Debug.WriteLine(xmllen);
                    
                   //// System.Diagnostics.Debug.Write("doc=" + encoder.GetString( ms.ToArray()));

                   // string xml1 = encoder.GetString(ms.ToArray());

                   // if (xml1.Trim() != string.Empty && xml1.StartsWith("<?xml")) 
                   // {
                   //     System.Diagnostics.Debug.Write("doc=" + xml1);

                   //     //XmlDocument xml = new XmlDocument();
                   //     //xml.LoadXml(xml1);

                   //     //XmlNodeList lst = xml.GetElementsByTagName("CarPlate");
                   //     //if (lst.Count > 0)
                   //     //{
                   //     //    System.Diagnostics.Debug.Write(lst[0].Value);
                   //     //}
                   // }

                    

                    //packpos =packpos + readLength;
                    
                }
                //else
                //{
                //    System.Diagnostics.Debug.WriteLine("Get Body ======================");
                //    if (packData.Length>0)
                //    {
                //        //System.Diagnostics.Debug.WriteLine("Get Body 1.1======================");
                //        System.Diagnostics.Debug.WriteLine("Pack length ======================" + packLength.ToString());
                //        System.Diagnostics.Debug.WriteLine("Pack Pos ======================" + packpos.ToString());
                //        if (packpos < packLength)
                //        {
                //            //System.Diagnostics.Debug.WriteLine("Get Body 1.1.1======================");
                //            long readLength = 0;
                //            if (packLength - packpos > message.Length)
                //            {
                //                readLength = message.Length;
                //            }
                //            else
                //            {
                //                readLength = packLength - packpos;
                //            }
                //            ///////////////////////////////////Buffer.BlockCopy(message, 0, packData, packpos, (int)readLength);
                //            ms.Write(message, 0, (int)readLength);
                //            //System.Diagnostics.Debug.WriteLine(encoder.GetString(packData));
                //            packpos = packpos + (int)readLength;
                //        }
                //        else
                //        {
                //            System.Diagnostics.Debug.WriteLine("Pack Pos ======================" + packpos.ToString());
                //            System.Diagnostics.Debug.WriteLine("Get Body 1.1.2======================");
                //            //Array.ForEach<byte>(packData, a => {
                //            //    System.Diagnostics.Debug.Write(Convert.ToChar(a));                            
                //            //});
                //            ms.Position = 0;
                //            System.Diagnostics.Debug.WriteLine(encoder.GetString( ms.ToArray()));
                //        }
                //    }
                //}
                
            }
            tcpClient.Close();
            System.Diagnostics.Debug.WriteLine("client.close");
        }

        public static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
    }
}
