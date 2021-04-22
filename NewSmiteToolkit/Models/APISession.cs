using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewSmiteToolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public static class APISession
    {

        public static SessionInfo sessionInfo = new SessionInfo();

        public static string GetSessionTimestamp()
        {
            DateTime timestamp = sessionInfo.timestamp;

            // create a new session if necessary
            if (timestamp == null || timestamp == DateTime.MinValue)
            {
                SessionInfo newSessionInfo = SpyToolUtils.CreateSession();
                Debug.WriteLine($"\nPast Timestamp is Invalid ({timestamp}).\nNew Timestamp is: {newSessionInfo.timestamp}");
                sessionInfo = newSessionInfo;

                SaveSessionInfo();
            }

            return sessionInfo.GetTimestampString();
        }

        public static string GetSessionId()
        {
            string signature = SpyToolUtils.GetSignature("testsession");

            Debug.WriteLine("\nChecking current session ....");

            string requestURI = SpyToolUtils.GetRequestURI("testsession", authParams: false, Credentials.DevId, signature, sessionInfo.session_id, GetSessionTimestamp());
            string responseFromServer = SpyToolUtils.GetServerResponse(requestURI);

            // create a new session if necessary
            if (responseFromServer == null || responseFromServer.Contains("Invalid") || responseFromServer.Contains("Exception"))
            {
                SessionInfo newSessionInfo = SpyToolUtils.CreateSession();
                Debug.WriteLine($"\nPast APISession Id was Invalid ({sessionInfo.session_id}).\nNew APISession Id is: {newSessionInfo.session_id}");
                sessionInfo = newSessionInfo;

                SaveSessionInfo();
            }
            else
            {
                Debug.WriteLine($"\n  Past APISession Id was Valid!");
            }

            return sessionInfo.session_id;
        }

        public static bool SaveSessionInfo()
        {
            //string path = @"..\..\..\Logs\SessionInfo.txt";
            string path = HttpContext.Current.Server.MapPath("~/Logs/SessionInfo.txt");

            if (!File.Exists(path))
            {
                Debug.WriteLine("Could not find SessionInfo file to save to");
                return false;
            }
            else
            {
                string jsonSessionInfo = JsonConvert.SerializeObject(sessionInfo);
                File.WriteAllText(path, jsonSessionInfo);
                return true;
            }
        }

        public static bool LoadSessionInfo()
        {
            //string path = @"..\..\..\Logs\SessionInfo.txt";
            string path = HttpContext.Current.Server.MapPath("~/Logs/SessionInfo.txt");

            if (!File.Exists(path))
            {
                Debug.WriteLine($"Could not find credentials file to load from ({path})");
                return false;
            }
            else
            {
                string jsonSessionInfo = File.ReadAllText(path);
                JObject jsonObject = JObject.Parse(jsonSessionInfo);

                SessionInfo newSessionInfo = new SessionInfo()
                {
                    ret_msg = jsonObject["ret_msg"].ToString(),
                    session_id = jsonObject["session_id"].ToString(),
                    timestamp = jsonObject["timestamp"].ToObject<DateTime>()
                };

                sessionInfo = newSessionInfo;

                return true;
            }
        }
    }
}