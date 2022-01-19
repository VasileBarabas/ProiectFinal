using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProiectFinal.Models
{
    public class RestaurantMenu
    {

        [Key]
        public int Id_menu { get; set; }
        public string denumireProdus { get; set; }
        public string descriereProdus { get; set; }
        public float pret { get; set; }

    }
    public class MenuDbContex : DbContext
    {
        public DbSet<RestaurantMenu> Menus { get; set; }
    }
}