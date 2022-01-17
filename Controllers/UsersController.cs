﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProiectFinal.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
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
        [HttpPost]
        public ActionResult Verify(User user)
        {
            //Redirectare
            var res = VerifyLogin(user);
            if(res.Result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Contul introdus nu exista, reincercati";
                return View("Login");
            }
         
        }
        public async Task<bool> VerifyLogin(User user)
        {
            //verificare bd
                using (UserDbContex udb = new UserDbContex())
                {
                StringBuilder hashedPass = new StringBuilder();
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
      
        public async Task InsertToDb(User user)
        {
 
                try
                {
                    using (UserDbContex udb = new UserDbContex())
                    {

                        var res = udb.Users.SingleOrDefault(u => u.Password == user.Password && u.Username == user.Username);
                    if (res != null)
                    {
                        ErrorMessage = "Contul exista deja, reincercati";

                    }
                    else
                    {

                        StringBuilder hashedPass = new StringBuilder();
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