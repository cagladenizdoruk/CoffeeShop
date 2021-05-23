using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeeShop.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<AppRole>(context);
                var manager = new RoleManager<AppRole>(store);
                var role = new AppRole() { Name = "admin", Description = "admin rolü" };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<AppRole>(context);
                var manager = new RoleManager<AppRole>(store);
                var role = new AppRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }
            if (!context.Users.Any(i => i.Name == "eceayvaz"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                var user = new AppUser()
                { Name = "Ece", Surname = "Ayvaz", UserName = "eceayvaz", Email = "eceayvaz@gmail.com" };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
           
            if (!context.Users.Any(i => i.Name == "cagla"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                var user = new AppUser()
                { Name = "Çağla Deniz", Surname = "Doruk", UserName = "cagla", Email = "cagladoruk@gmail.com" };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "user");
            }
            base.Seed(context);
        }

    }
}