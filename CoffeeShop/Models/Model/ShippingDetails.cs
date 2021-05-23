using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models.Model
{
    public class ShippingDetails
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Add Your Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Add Your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Add Your District")]
        public string District { get; set; }
        [Required(ErrorMessage = "Please Add Your Street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Please Add Your Postcode")]
        public string Postcode { get; set; }
    }
}