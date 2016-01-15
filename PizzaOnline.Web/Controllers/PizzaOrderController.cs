using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaOnline.Web.Controllers
{
    public class PizzaOrderController : Controller
    {
        // GET: PizzaOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}