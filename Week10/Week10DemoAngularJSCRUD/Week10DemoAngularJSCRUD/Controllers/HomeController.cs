using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Week10DemoAngularJSCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Book()
        {
            return View();
        }

        public ActionResult Unit()
        {
            return View();
        }
    }
}