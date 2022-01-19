using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProiectFinal.Models;

namespace ProiectFinal.Controllers
{
    public class RestaurantMenuController : Controller
    {
        // GET: RestaurantMenu
        private CosDbContext cdb = new CosDbContext();
        private MenuDbContex mdb = new MenuDbContex();
        public ActionResult Index()
        {
            return View(mdb.Menus.ToList());
        }
        [HttpPost]
        public ActionResult Restaurant()
        {
            return View(mdb.Menus.ToList());
        }
        [HttpPost]
        public ActionResult Adauga(string numeProdus)
        {
        
            if (ModelState.IsValid)
            {
                Cos cos = new Cos();
                RestaurantMenu menu = mdb.Menus.SingleOrDefault(m=>m.denumireProdus == numeProdus); 
                if(menu != null) {
                    cos.denumire = menu.denumireProdus;
                    cos.pret = menu.pret;
                }
                
                cdb.Cumparaturi.Add(cos);
                cdb.SaveChanges();

                
            }
            return View("Confirmare");
        }
    }


}