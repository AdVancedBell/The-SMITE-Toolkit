using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Constants
{
    public static class EnumExtensions
    {
        public static string GetId(this Enum enumObj)
        {
            return ((int)(object)enumObj).ToString();
        }
    }
}