using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class Player
    {
        public string player_id { get; set; }
        public string portal { get; set; }
        public int portal_id { get; set; }
        public string privacy_flag { get; set; }
        public string ret_msg { get; set; }
    }
}