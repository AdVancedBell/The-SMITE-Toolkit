using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewSmiteToolkit.Constants;
using NewSmiteToolkit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace NewSmiteToolkit.Utilities
{
    public static class SpyToolUtils
    {
        public static string Endpoint => "http://api.smitegame.com/smiteapi.svc";
        public static string ResponseFormat => "Json";

        //From HiRez vvv
        public static string GetMD5Hash(string input)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            bytes = md5.ComputeHash(bytes);
            var sb = new System.Text.StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        //From HiRez ^^^

        public static string GetNewTimestamp()
        {
            return String.Format("{0:yyyyMMddHHmmss}", DateTime.UtcNow);
        }

        public static string GetSignature(string apiCall, bool newTimestamp = false)
        {
            string timestamp;

            if (newTimestamp)
            {
                timestamp = GetNewTimestamp();
            }
            else
            {
                timestamp = APISession.GetSessionTimestamp();
            }

            string signature = GetMD5Hash(Credentials.DevId + apiCall + Credentials.AuthKey + timestamp);

            return signature;
        }

        public static string GetServerResponse(string requestURI)
        {
            WebRequest request = WebRequest.Create(requestURI);
            string responseFromServer = null;

            try
            {
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader dataReader = new StreamReader(dataStream);
                responseFromServer = dataReader.ReadToEnd();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"\nAn error occurred while retrieving the server response from ({requestURI}) \n   {e}");
            }

            return responseFromServer;
        }

        public static string GetRequestURI(string apiCall, bool authParams = false, params string[] parameters)
        {

            string requestURI = $"{Endpoint}/{apiCall}Json";

            if (authParams)
            {
                string sessionId = APISession.GetSessionId();

                requestURI += $"/{Credentials.DevId}";
                requestURI += $"/{GetSignature(apiCall)}";
                requestURI += $"/{sessionId}";
                requestURI += $"/{APISession.GetSessionTimestamp()}";
            }

            foreach (var p in parameters)
            {
                requestURI += $"/{p}";
            }

            return requestURI;
        }

        public static SessionInfo CreateSession()
        {
            Debug.WriteLine("\nCreating a new session ....");

            string signature = GetSignature("createsession", newTimestamp: true);
            string requestURI = GetRequestURI("createsession", authParams: false, Credentials.DevId, signature, GetNewTimestamp());
            string serverResponse = GetServerResponse(requestURI);
            JObject jsonObject = JObject.Parse(serverResponse);

            SessionInfo sessionInfo = new SessionInfo()
            {
                ret_msg = jsonObject["ret_msg"].ToString(),
                session_id = jsonObject["session_id"].ToString(),
                timestamp = jsonObject["timestamp"].ToObject<DateTime>()
            };

            return sessionInfo;
        }

        public static DetailPlayer GetPlayer(string portalId, string gamertag)
        {
            //string requestURI = GetRequestURI("getplayeridsbygamertag", authParams: true, portalId, gamertag);
            //string serverResponse = GetServerResponse(requestURI);
            //List<Player> player = JsonConvert.DeserializeObject<List<Player>>(serverResponse);

            string requestURI = SpyToolUtils.GetRequestURI("getplayer", authParams: true, gamertag, portalId);
            string responseFromServer = SpyToolUtils.GetServerResponse(requestURI);

            List<DetailPlayer> player = JsonConvert.DeserializeObject<List<DetailPlayer>>(responseFromServer);

            return player.FirstOrDefault();
        }

        public static List<string> GetActiveMatchIds(Gamemode mode, int minutesSinceMatchStart = 0)
        {
            DateTime now = DateTime.UtcNow;

            now = now.AddMinutes(-minutesSinceMatchStart);

            string date = String.Format("{0:yyyyMMdd}", now);
            string hour = String.Format("{0:HH}", now);
            int minuteSpan = ((int)(now.Minute / 10)) * 10;
            string hourWithMinutes = hour + $",{minuteSpan}";

            string requestURI = GetRequestURI("getmatchidsbyqueue", authParams: true, mode.GetId(), date, hourWithMinutes);
            string serverResponse = GetServerResponse(requestURI);

            List<MatchMetadata> matchMetadataList = JsonConvert.DeserializeObject<List<MatchMetadata>>(serverResponse);

            List<string> activeMatchIds = matchMetadataList.Where(m => m.Active_Flag.Equals("y")).Select(m => m.Match).ToList();
            Debug.WriteLine($"\nActive Match Count ({mode}) = {activeMatchIds.Count}");

            return activeMatchIds;
        }

        public static string FindMatchIdByPlayerId(string playerId, List<string> matchIds)
        {
            string matchId = null;
            List<MatchPlayer> players = new List<MatchPlayer>();

            foreach (var id in matchIds)
            {
                string requestURI = GetRequestURI("getmatchplayerdetails", authParams: true, id);
                string serverResponse = GetServerResponse(requestURI);
                players = JsonConvert.DeserializeObject<List<MatchPlayer>>(serverResponse);

                //if (players.Any(p => p.playerId == null))
                //{

                //    Debug.WriteLine("ERROR: Player had null ID.");

                //    continue;
                //}

                foreach (var player in players)
                {
                    if (player.playerId == null)
                    {
                        continue;
                    }
                    if (player.playerId.Equals(playerId))
                    {
                        matchId = id;
                        break;
                    }
                }

                if (matchId != null)
                {
                    break;
                }
            }

            return matchId;
        }
    }
}