﻿using CoffeeShop.Identity;
using CoffeeShop.Models.DataContext;
using CoffeeShop.Models.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountController : Controller
    {
        CShopDBContext db = new CShopDBContext();

        private UserManager<AppUser> UserManager;
        private RoleManager<AppRole> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<AppUser>(new IdentityDataContext());
            UserManager = new UserManager<AppUser>(userStore);

            var roleStore = new RoleStore<AppRole>(new IdentityDataContext());
            RoleManager = new RoleManager<AppRole>(roleStore);
        }
        // GET: Account
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return View("Update");
            }
            return View(model);
        }
        public PartialViewResult UserCount()
        {
            var u = UserManager.Users;
            return PartialView(u);
        }
        public ActionResult UserList()
        {
            var u = UserManager.Users;
            return View(u);
        }
        public ActionResult UserProfil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            var data = new UserProfile()
            {
                id=user.Id,
                Name=user.Name,
                Surname=user.Surname,
                Email=user.Email,
                Username=user.UserName
            };
            return View(data);
        }
        [HttpPost]
        public ActionResult UserProfil(UserProfile model)
        {
            var user = UserManager.FindById(model.id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;
            UserManager.Update(user);
            return View("Update");
        }
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var Identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, Identityclaims);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "User not found.");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError","Register User Error...");
                }
            }
            return View(model);
        }

        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var order=db.Order.Where(i=>i.UserName==username).Select(i=> new UserOrder
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                OrderDate = i.OrderDate,
                Total = i.Total
            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(order);
            
        }
        public ActionResult Details(int id)
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

           
        }
    }
