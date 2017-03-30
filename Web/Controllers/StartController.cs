using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class StartController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Owners And Pets";

            return View();
        }
    }
}
