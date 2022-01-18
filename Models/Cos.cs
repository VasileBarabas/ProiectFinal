using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProiectFinal.Models
{
    public class Cos
    { 
        [Key]
        public int Id { get; set; }
        public string denumire { get; set; }
        public float pret { get; set; }

  
    }
    public class CosDbContext : DbContext
    {
        public DbSet<Cos> Cumparaturi { get; set; }
    }
}