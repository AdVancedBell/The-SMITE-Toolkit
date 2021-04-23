using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class GodQueueStats
    {
        public string Assists { get; set; }
        public string Deaths { get; set; }
        public string God { get; set; }
        public string GodId { get; set; }
        public string Kills { get; set; }
        public string Losses { get; set; }
        public string Matches { get; set; }
        public string Minutes { get; set; }
        public string Queue { get; set; }
        public string Wins { get; set; }
        public string player_id { get; set; }
        public string ret_msg { get; set; }

        public double wl_val
        {
            get
            {
                return double.Parse(Wins) / Math.Max(1, double.Parse(Losses));
            }
        }
        public string wl_str
        {
            get
            {
                return String.Format(wl_val % 1 == 0 ? "{0:0}" : "{0:0.00}", wl_val);
            }
        }

        public double kd_val
        {
            get
            {
                return double.Parse(Kills) / Math.Max(1, double.Parse(Deaths));
            }
        }
        public string kd_str
        {
            get
            {
                return String.Format(kd_val % 1 == 0 ? "{0:0}" : "{0:0.00}", kd_val);
            }
        }

        public double kda_val
        {
            get
            {
                return (double.Parse(Kills) + double.Parse(Assists)) / Math.Max(1, double.Parse(Deaths));
            }
        }
        public string kda_str
        {
            get
            {
                return String.Format(kda_val % 1 == 0 ? "{0:0}" : "{0:0.00}", kda_val);
            }
        }


    }
}