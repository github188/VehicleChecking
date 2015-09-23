using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VehicleChecking
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class Vehinfo
    {
        /// <summary>
        /// 好牌颜色
        /// </summary>
        public string PlateColor { get; set; }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string CarPlate { get; set; }

        public bool IsMatched { get; set; }

        public Vehinfo(string xml)
        {
            IsMatched = false;

            Regex reg = new Regex(@"<CarPlate\>(.*?)\<\/CarPlate\>");
            Match match = reg.Match(xml);
            if (match.Success)
            {
                string vehNo = match.Value.Replace(@"<CarPlate>", "").Replace(@"</CarPlate>", "");
                if (vehNo.Trim() != string.Empty)
                {
                    this.CarPlate = vehNo.Trim();
                    //System.Diagnostics.Debug.WriteLine(vehNo.Trim());
                    IsMatched = true;
                }
            }

            reg = new Regex(@"<PlateColor\>(.*?)\<\/PlateColor\>");
            match = reg.Match(xml);
            if (match.Success)
            {
                string vehNo = match.Value.Replace(@"<PlateColor>", "").Replace(@"</PlateColor>", "");
                if (vehNo.Trim() != string.Empty)
                {
                    this.CarPlate = vehNo.Trim();
                    //System.Diagnostics.Debug.WriteLine(vehNo.Trim());
                    IsMatched = true;
                }
            }
        }
    }
}
