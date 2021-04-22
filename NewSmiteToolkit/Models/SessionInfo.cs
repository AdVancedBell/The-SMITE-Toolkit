using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class SessionInfo
    {
        public string ret_msg { get; set; }
        public string session_id { get; set; }
        public DateTime timestamp { get; set; }

        public SessionInfo()
        {
            ret_msg = "EmptySessionInfo";
            session_id = "EmptySessionInfo";
            timestamp = DateTime.MinValue;
        }

        public string GetTimestampString()
        {
            return String.Format("{0:yyyyMMddHHmmss}", timestamp);
        }
    }
}