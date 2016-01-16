using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaOnline.Web.Controllers
{
    public class PizzaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compose()
        {
            return View();
        }
    }
}