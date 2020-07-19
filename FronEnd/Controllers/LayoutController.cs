using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }
    }
}