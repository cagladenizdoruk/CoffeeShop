using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public enum OrderState
    {
        Waiting,
        Completed,
        Packaged,
        Shipped
    }
}