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


    }
}