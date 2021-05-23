using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderState OrderState { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public virtual List<OrderLine> OrdeLines { get; set; }

        public class OrderLine
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public virtual Order Order { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public int ProductId { get; set; }
            public virtual Product Product { get; set; }
        }
    }
}