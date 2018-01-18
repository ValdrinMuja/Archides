using Archides.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Archides.Controllers
{
    public class HomeController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();


        //Metodat per Userin e thjeshte
        public ActionResult News()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About View.";

            return View();
        }


        public ActionResult Gallery()
        {
            ViewBag.Message = "Gallery View";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact View.";

            return View();
        }

        public ActionResult OurTeam()
        {
            return View();
        }
        //Fundi Metodat per Userin e thjeshte



    }
}