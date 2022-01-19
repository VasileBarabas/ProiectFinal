using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectFinal.Models;

namespace ProiectFinal.Controllers
{
    
    public class CosController : Controller
    {
        
        private CosDbContext dbContext = new CosDbContext();

        

        [HttpGet]
        public ActionResult Cos()
        {
            return View(dbContext.Cumparaturi.ToList());


        }

     

        public ActionResult Delete(string numeProdus)
        {
            if (ModelState.IsValid)
            {
                
                Cos cos = dbContext.Cumparaturi.SingleOrDefault(m => m.denumire == numeProdus);
                if (cos != null)
                {
                    dbContext.Cumparaturi.Remove(cos);
                }

                dbContext.SaveChanges();

            }
            return View("Confirmare");
        }
        
        public ActionResult Comanda(string nume )
        {
                    var cumparaturi = from m in dbContext.Cumparaturi
                                      select m;
                    foreach (var cumparatura in cumparaturi)
                    {
                        dbContext.Cumparaturi.Remove(cumparatura);
                    }
                    dbContext.SaveChanges();

            return View("Confirmare2");
        }
    }
}