using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProiectFinal.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ProiectFinal.Controllers
{
    public class UsersController : Controller
    {
        
        public string ErrorMessage;

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Checking if the users exists
        [HttpPost]
        public ActionResult Verify(User user)
        {
            //Redirectare
            var res = VerifyLogin(user);
            DateTime now = DateTime.Now;
            if (res.Result)
            {
                Trace.WriteLine(user.Username + " s-a logat: "+ now.ToString("F"));
                using (CosDbContext dbContext = new CosDbContext())
                {
                    var cumparaturi = from m in dbContext.Cumparaturi
                                      select m;
                    foreach(var cumparatura in cumparaturi)
                    {
                        dbContext.Cumparaturi.Remove(cumparatura);
                    }
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Trace.WriteLine("Eroare logare: " + now.ToString("F"));
                ViewBag.Message = "Contul introdus nu exista, reincercati";
                return View("Login");
            }
         
        }
        //Async method to check if the users is in the db
        public async Task<bool> VerifyLogin(User user)
        {
            //verificare bd
                using (UserDbContex udb = new UserDbContex())
                {
                StringBuilder hashedPass = new StringBuilder();
                //hashing the password so we can search it in the db
                using (SHA256 hash = SHA256.Create())
                {
                    byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                    for (int i = 0; i < bytes.Length; ++i)
                    {

                        hashedPass.Append(bytes[i].ToString("x2"));
                    }

                }
                user.Password = hashedPass.ToString();
                var res = udb.Users.SingleOrDefault(u => u.Password == user.Password && u.Username == user.Username);
                    if (res != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToDb(User user)
        {
            Console.WriteLine("Vream Sa bagam in db");

            if (ModelState.IsValid)
            {
               var res= InsertToDb(user);
                ViewBag.Message = ErrorMessage;
                
            }
            return View("SignUp");
        }
      //Async method to add the user to the database
        public async Task InsertToDb(User user)
        {
 
                try
                {
                    using (UserDbContex udb = new UserDbContex())
                    {

                        var res = udb.Users.SingleOrDefault(u => u.Password == user.Password && u.Username == user.Username);
                        //Checking if the user does already exists or not( user+password)
                    if (res != null)
                    {
                        ErrorMessage = "Contul exista deja, reincercati";

                    }
                    else
                    {

                        StringBuilder hashedPass = new StringBuilder();
                         //hashing the password so we can insert it in the db
                        using (SHA256 hash = SHA256.Create())
                        {
                            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                        
                            for (int i=0;i<bytes.Length;++i)
                            {
                               
                                 hashedPass.Append(bytes[i].ToString("x2"));
                            }
                            
                        }
                        user.Password = hashedPass.ToString();
                        udb.Users.Add(user);
                        udb.SaveChanges();
                        ErrorMessage = "Cont inserat cu succes";
                    }
                    }
                }
                catch(Exception e)
                {
                Console.WriteLine(e.Message);
                }
        }
    }
  
}
