using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public class StateModel
    {
        public int ProductNumber { get; set; }
        public int OrderNumber { get; set; }
        public int WaitingOrderNumber { get; set; }
        public int CompletedOrderNumber { get; set; }
        public int PackagedOrderNumber { get; set; }
        public int ShippedOrderNumber { get; set; }
    }
}