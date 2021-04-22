using Newtonsoft.Json;
using NewSmiteToolkit.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewSmiteToolkit.Models;
using NewSmiteToolkit.Utilities;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace NewSmiteToolkit.Controllers
{
    public class SpyToolController : Controller
    {
        // GET: SpyTool
        public ActionResult Index()
        {
            ViewBag.Message = "The SMITE Spy Tool!";

            return View();
        }

        [HttpPost]
        public ActionResult Results(string username, string platform, string gamemode, string minutesBehind)
        {
            // TEMP vvv
            ViewBag.Message = JsonConvert.SerializeObject(new { username, platform, gamemode, minutesBehind });
            // TEMP ^^^

            // validate access to API
            string requestURI = SpyToolUtils.GetRequestURI("ping");
            string responseFromServer = SpyToolUtils.GetServerResponse(requestURI);
            if (responseFromServer == null)
            {
                // ERROR Server is down
                Debug.WriteLine($"\n   ERROR: Ping to server was unsuccesful");
                return View();
            }
            Debug.WriteLine($"\n{responseFromServer}");

            // Load previous session info from memory
            APISession.LoadSessionInfo();
            Debug.WriteLine($"\nSessionInfo in Memory: \n  RetMsg = {APISession.sessionInfo.ret_msg} \n  SessionId = {APISession.sessionInfo.session_id} \n  Timestamp = {APISession.sessionInfo.timestamp}");

            // Find Player from username/portal input
            Portal portal;
            Enum.TryParse<Portal>(platform, out portal);
            Player player = SpyToolUtils.GetPlayer(portal.GetId(), username);
            Debug.WriteLine($"\n  Username: {username}\n  Player ID: {player.player_id}");

            // TEMP vvv
            string id = player.player_id;
            ViewBag.Message = JsonConvert.SerializeObject(new { username, id });
            // TEMP ^^^

            // Retrieve all current matches modified by (NOW - minnutesBehind)
            Gamemode mode;
            Enum.TryParse<Gamemode>(gamemode, out mode);
            List<string> activeMatchIds = SpyToolUtils.GetActiveMatchIds(mode, minutesSinceMatchStart: int.Parse(minutesBehind));

            // Find match that Player is in
            string matchId = SpyToolUtils.FindMatchIdByPlayerId(player.player_id, activeMatchIds);

            if (matchId == null)
            {
                Debug.WriteLine("\nCould not find player's match.");
            }
            else
            {
                Debug.WriteLine($"\nFound Match! Match ID: {matchId}");
            }

            // Request Match Details of Found Match
            requestURI = SpyToolUtils.GetRequestURI("getmatchplayerdetails", authParams: true, matchId);
            responseFromServer = SpyToolUtils.GetServerResponse(requestURI);

            List<MatchPlayer> matchPlayers = JsonConvert.DeserializeObject<List<MatchPlayer>>(responseFromServer);

            //// save to disk for later testing : TESTING ONLY
            //responseFromServer = SpyToolUtils.GetServerResponse(requestURI);
            //string path = Server.MapPath("~/Logs/TestMatchPlayerDetails.txt");
            //if (!System.IO.File.Exists(path))
            //{
            //    Debug.WriteLine("Could not find TestMatchPlayerDetails file to save to");
            //}
            //else
            //{
            //    System.IO.File.WriteAllText(path, responseFromServer);
            //}

            //// Load from disk : TESTING
            //string path = Server.MapPath("~/Logs/TestMatchPlayerDetails.txt");
            //List<MatchPlayer> matchPlayers = new List<MatchPlayer>();

            //if (!System.IO.File.Exists(path))
            //{
            //    Debug.WriteLine($"Could not find credentials file to load from ({path})");
            //}
            //else
            //{
            //    string jsonPlayerInfo = System.IO.File.ReadAllText(path);

            //    matchPlayers = JsonConvert.DeserializeObject<List<MatchPlayer>>(jsonPlayerInfo);
            //}

            return View(matchPlayers);
        }

        public ActionResult PlayerDetails(string playerJson)
        {
            ViewBag.Message = playerJson;

            MatchPlayer matchPlayer = JsonConvert.DeserializeObject<MatchPlayer>(playerJson);

            return View(matchPlayer);
        }
    }
}