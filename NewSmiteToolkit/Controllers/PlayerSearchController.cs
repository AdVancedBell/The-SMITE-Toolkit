using NewSmiteToolkit.Constants;
using NewSmiteToolkit.Models;
using NewSmiteToolkit.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSmiteToolkit.Controllers
{
    public class PlayerSearchController : Controller
    {
        // GET: PlayerSearch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlayerDetails(string username, string platform)
        {
            //temp vvv
            ViewBag.Message = username;
            //temp ^^^

            try
            {
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

                requestURI = SpyToolUtils.GetRequestURI("getplayer", authParams: true, username, portal.GetId());
                responseFromServer = SpyToolUtils.GetServerResponse(requestURI);

                List<DetailPlayer> playerList = JsonConvert.DeserializeObject<List<DetailPlayer>>(responseFromServer);

                DetailPlayer player = playerList[0];
                Debug.WriteLine($"\n  Username: {username}\n  Player ID: {player.Id}");

                return View(player);
            }
            catch (Exception e)
            {
                return Redirect(Url.Action("Index", "Error", new { error = e.Message }));
            }

        }
    }
}