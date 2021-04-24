using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSmiteToolkit.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string error = "")
        {
            ViewBag.Message = error;
            return View();
        }
    }
}