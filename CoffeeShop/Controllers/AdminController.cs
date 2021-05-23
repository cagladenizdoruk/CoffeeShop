using CoffeeShop.Models.DataContext;
using CoffeeShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CShopDBContext db = new CShopDBContext();
            
        public ActionResult Index()
        {
            StateModel model = new StateModel();
            model.WaitingOrderNumber = db.Order.Where(i => i.OrderState == OrderState.Waiting).ToList().Count();
            model.CompletedOrderNumber = db.Order.Where(i => i.OrderState == OrderState.Completed).ToList().Count();
            model.PackagedOrderNumber = db.Order.Where(i =>i.OrderState == OrderState.Packaged).ToList().Count();
            model.ShippedOrderNumber = db.Order.Where(i => i.OrderState == OrderState.Shipped).ToList().Count();
            model.ProductNumber = db.Product.Count();
            model.OrderNumber = db.Order.Count();

            return View(model);
        }
        public PartialViewResult NotificationMenu()
        {
            var notification = db.Order.Where(i => i.OrderState == OrderState.Waiting).ToList();
            return PartialView(notification);
        }
    }
}