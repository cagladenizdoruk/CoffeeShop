using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Identity
{
    public class AppRole: IdentityRole
    {
        public string Description { get; set; }
        public AppRole()
        {

        }
        public AppRole(string rolename, string description)
        {
            this.Description = description;
        }
    }
}