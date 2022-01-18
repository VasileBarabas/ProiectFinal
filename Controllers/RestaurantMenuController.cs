using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectFinal.Controllers
{
    public class RestaurantMenuController : Controller
    {
        // GET: RestaurantMenu
        public ActionResult Index()
        {
            return View();
        }
    }
}