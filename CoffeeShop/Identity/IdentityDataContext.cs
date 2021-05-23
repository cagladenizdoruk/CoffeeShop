using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShop.Identity
{
    public class IdentityDataContext : IdentityDbContext<AppUser>
    {
        public IdentityDataContext() : base("IdentityConnection")
        {

        }
    }
}