using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace VehicleChecking
{
    public class SettingOption
    {
        public const string SETTING_FILE_NAME = "setting.cofing";

        #region led info

        public string FontFamily { get; set; }
        public double FontSize { get; set; }
        public int NormalColor { get; set; }
        public int AlarmColor { get; set; }
        public int BackgroundColor { get; set; }
        public int LeftMargin { get; set; }
        public int TopMargin { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        #endregion

        #region VPInfo

        public int LocalPort { get; set; }
        public string LocalIp { get; set; }

        #endregion

        #region Server info

        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string ServerUsername { get; set; }
        public string ServerPassword { get; set; }

        public int ServerIntelval { get; set; }

        #endregion

        #region Sound Info

        public string SoundPath { get; set; }
        public string ValidText { get; set; }

        #endregion

        #region Method

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Save()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = System.IO.Path.Combine(path,SETTING_FILE_NAME);
           
            System.IO.FileStream stream;
            stream = System.IO.File.Create(path);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(this.ToString());
            writer.Flush();
            writer.Close();
            stream.Dispose();
        }

        public static SettingOption Load()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = System.IO.Path.Combine(path,SETTING_FILE_NAME);

            SettingOption setting = new SettingOption();
            if (System.IO.File.Exists(path))
            {
                System.IO.StreamReader reader = System.IO.File.OpenText(path);
                string json = reader.ReadToEnd();
                reader.Close();
                setting = JsonConvert.DeserializeObject<SettingOption>(json);
            }
            return setting;
        }

        #endregion
    }
}
