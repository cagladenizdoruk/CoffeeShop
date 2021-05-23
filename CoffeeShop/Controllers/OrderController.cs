using CoffeeShop.Models.DataContext;
using CoffeeShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class OrderController : Controller
    {
        CShopDBContext db = new CShopDBContext();

        public ActionResult Index()
        {
            var order = db.Order.Select(i => new AdminOrder()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total,
                Count = i.OrdeLines.Count
            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(order);
        }
        public ActionResult Details (int id)
        {

            var model = db.Order.Where(i => i.Id == id).Select(i => new OrderDetails()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Address = i.Address,
                City = i.City,
                District = i.District,
                Street = i.Street,
                Postcode = i.Postcode,
                OrderLines = i.OrdeLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.Image,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            }).FirstOrDefault();



            return View(model);
        }
        public ActionResult UpdateOrderState(int OrderId, OrderState Orderstate)
        {
            var order = db.Order.FirstOrDefault(i => i.Id == OrderId);
            if (order != null)
            {
                order.OrderState = Orderstate;
                db.SaveChanges();
                TempData["message"] = "Information is recorded.";
                return RedirectToAction("Details", new { id = OrderId });
            }
            return RedirectToAction("Index");
        }
        public ActionResult WaitingOrders()
        {
            var model = db.Order.Where(i => i.OrderState == OrderState.Waiting).ToList();
            return View(model);
        }
        public ActionResult CompletedOrders()
        {
            var model = db.Order.Where(i => i.OrderState == OrderState.Completed).ToList();
            return View(model);
        }
        public ActionResult PackagedOrders()
        {
            var model = db.Order.Where(i => i.OrderState == OrderState.Packaged).ToList();
            return View(model);
        }
        public ActionResult ShippedOrders()
        {
            var model = db.Order.Where(i => i.OrderState == OrderState.Shipped).ToList();
            return View(model);
        }
    }
}