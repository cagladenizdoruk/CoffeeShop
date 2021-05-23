using CoffeeShop.Models.DataContext;
using CoffeeShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CoffeeShop.Models.Model.Order;

namespace CoffeeShop.Controllers
{
    public class BasketController : Controller
    {
        CShopDBContext db = new CShopDBContext();
        public ActionResult Index()
        {
            return View(GetBasket());
        }
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails model)
        {
            var basket = GetBasket();
            if (basket.BasketLines.Count == 0)
            {
                ModelState.AddModelError("No Product", " Your basket is empty.");
            }
            if (ModelState.IsValid)
            {
                SaveOrder(basket, model);
                basket.Clear();
                return View("OrderCompleted");
            }
            else
            {
                return View(model);
            }

        }

        private void SaveOrder(Basket basket, ShippingDetails model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString();
            order.Total = basket.Total();
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;
            order.OrderState = OrderState.Waiting;
            order.Address = model.Address;
            order.City = model.City;
            order.District = model.District;
            order.City = model.City;
            order.Postcode = model.Postcode;
            order.OrdeLines = new List<OrderLine>();
            foreach (var item in basket.BasketLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.ProductId;
                order.OrdeLines.Add(orderline);
            }
            db.Order.Add(order);
            db.SaveChanges();
        }



        public PartialViewResult Summary()
        {
            return PartialView(GetBasket());
        }
        public ActionResult RemoveFromBasket(int Id)
        {
            var product = db.Product.FirstOrDefault(i => i.ProductId == Id);
            if (product != null)
            {
                GetBasket().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddToBasket(int Id)
        {
            var product = db.Product.FirstOrDefault(i => i.ProductId == Id);
            if (product != null)
            {
                GetBasket().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }
        public Basket GetBasket()
        {
            var basket = (Basket)Session["Basket"];
            if (basket == null)
            {  
                    basket = new Basket();
                    Session["Basket"] = basket;
            }
                return basket;
        }
        





    }
}