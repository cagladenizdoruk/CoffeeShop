
using CoffeeShop.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        CShopDBContext db = new CShopDBContext();
       
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult MenuDetails(int Id)
        {
            return View(db.Product.Where(i=>i.ProductId==Id).FirstOrDefault());
        }
        public ActionResult Menu()
        {
            return View(db.Product.Where(i=>i.IsApproved).ToList());
        }
        public ActionResult ProductList(int Id)
        {
            return View(db.Product.Where(i => i.CategoryId ==Id).ToList());
        }
        public ActionResult HomeAbout()
        {
            return View(db.About.FirstOrDefault());
        }
        
    }
}