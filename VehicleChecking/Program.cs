using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VehicleChecking
{
    static class Program
    {
        public static Queue<string> VehicleCheckingQueue;
        public static List<string> BlackList;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            VehicleCheckingQueue = new Queue<string>();
            BlackList = new List<string>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmConsole());
        }
    }
}
