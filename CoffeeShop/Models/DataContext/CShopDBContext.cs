using CoffeeShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static CoffeeShop.Models.Model.Order;

namespace CoffeeShop.Models.DataContext
{
    public class CShopDBContext:DbContext
    {
        public CShopDBContext() : base("CShopDB")
        {

        }
        public DbSet<About> About { get; set; }
        public DbSet<Category> Category { get; set; }
       
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
        public object Products { get; internal set; }
    }
}
    
