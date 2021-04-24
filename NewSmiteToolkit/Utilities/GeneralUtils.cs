using NewSmiteToolkit.Constants;
using NewSmiteToolkit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Utilities
{
    public static class GeneralUtils
    {

        public static List<God> GetGods()
        {
            string requestUri = SpyToolUtils.GetRequestURI("getgods", authParams: true, LanguageCode.English.GetId());
            string serverResponse = SpyToolUtils.GetServerResponse(requestUri);

            List<God> gods = JsonConvert.DeserializeObject<List<God>>(serverResponse);
            gods.OrderBy(g => g.Name).ToList();

            return gods;
        }

    }
}