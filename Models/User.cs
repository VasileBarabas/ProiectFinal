using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ProiectFinal.Models
{
    public class User
    { 
        //Test git
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class UserDbContex : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}