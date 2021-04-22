using Newtonsoft.Json;
using NewSmiteToolkit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewSmiteToolkit.Controllers
{
    public class GodController : Controller
    {

        private GodsDBContext db = new GodsDBContext();

        // GET: God
        public ActionResult Index()
        {
            var gods = from g in db.Gods
                       orderby g.Name
                       select g;
            return View(gods);
        }

        //GET: God/Details/[id]
        public ActionResult Details(int id)
        {
            //AvgStats("Mana", g => g.Mana, g => g.ManaPerLevel);

            return View(db.Gods.FirstOrDefault(g => g.id == id));
        }

        //used to quickly find max stats
        [NonAction]
        public void MaxMinStats(string attribute, Func<God, double> f1, Func<God, double> f2)
        {
            double beststat = double.MinValue;
            double worststat = double.MaxValue;
            God bestgod = null;
            God worstgod = null;

            foreach (var god in db.Gods)
            {
                double lvl20stat = f1(god) + (f2(god) * 20);

                // add conditions if some gods should not be considered

                if (lvl20stat < worststat && lvl20stat > 0)
                {
                    worststat = lvl20stat;
                    worstgod = god;
                }
                if (lvl20stat > beststat)
                {
                    beststat = lvl20stat;
                    bestgod = god;
                }
            }

            Debug.WriteLine($"\nLowest Lvl 20 [{attribute}] is {worstgod.Name} with a value of {worststat}");
            Debug.WriteLine($"Highest Lvl 20 [{attribute}] is {bestgod.Name} with a value of {beststat}");
        }

        [NonAction]
        public void AvgStats(string attribute, Func<God, double> f1, Func<God, double> f2)
        {
            double sum = 0;
            double count = 0;

            foreach (var god in db.Gods)
            {
                // add condition if some gods should not be considered
                sum += (f1(god) + (f2(god) * 20));
                count++;
            }

            Debug.WriteLine($"\nAverage Lvl 20 [{attribute}] is {sum / count}");
        }
    }
}