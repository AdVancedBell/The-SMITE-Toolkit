using Newtonsoft.Json;
using NewSmiteToolkit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NewSmiteToolkit.Utilities;
using NewSmiteToolkit.Constants;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewSmiteToolkit.Controllers
{
    public class GodController : Controller
    {

        //private GodsDBContext db = new GodsDBContext();

        // GET: God
        public ActionResult Index()
        {
            try
            {
                return View(GeneralUtils.GetGods());
            }
            catch (Exception e)
            {
                return Redirect(Url.Action("Index", "Error", new { error = e.Message }));
            }
        }

        //GET: God/Details/[id]
        public ActionResult Details(int id)
        {
            try
            {
                return View(GeneralUtils.GetGods().FirstOrDefault(g => g.id == id));
            }
            catch (Exception e)
            {
                return Redirect(Url.Action("Index", "Error", new { error = e.Message }));
            }
        }

        //used to quickly find max stats - Testing only
        [NonAction]
        public void MaxMinStats(string attribute, Func<God, double> f1, Func<God, double> f2)
        {
            double beststat = double.MinValue;
            double worststat = double.MaxValue;
            God bestgod = null;
            God worstgod = null;

            foreach (var god in GeneralUtils.GetGods())
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

            foreach (var god in GeneralUtils.GetGods())
            {
                if (f1(god) == 0)
                {
                    continue;
                }
                // add condition if some gods should not be considered
                sum += (f1(god) + (f2(god) * 20));
                count++;
            }

            Debug.WriteLine($"\nAverage Lvl 20 [{attribute}] is {sum / count}");
        }
    }
}