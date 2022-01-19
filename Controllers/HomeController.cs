using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProiectFinal.Models;
namespace ProiectFinal.Controllers
{
    public class HomeController : Controller
    {
        private CosDbContext dbContext = new CosDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Cos()
        {

            return View(dbContext.Cumparaturi.ToList());
        }

        public ActionResult Restaurant()
        {
            return View();
        }
    }
}