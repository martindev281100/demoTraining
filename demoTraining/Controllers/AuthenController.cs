using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoTraining.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace demoTraining.Controllers
{
    public class AuthenController : Controller
    {
        // GET: Authen
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                if (acc.Password.Equals(acc.ConfirmPassword))
                {
                    var userStore = new UserStore<IdentityUser>();
                    var manager = new UserManager<IdentityUser>(userStore);

                    var user = new IdentityUser() { UserName = acc.UserName };

                    IdentityResult result = manager.Create(user, acc.Password);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Error Adding new User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Confirm Password not the same as password");
                }
            }
            return View(acc);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account acc)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = manager.Find(acc.UserName, acc.Password);

            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties { }, userIdentity);
                return RedirectToAction("Index", "Courses");
            }
            return View(acc);
        }

        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Authen");
        }
        public static void CreateAccount(string UserName, string Password, string role)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser(UserName);
            manager.Create(user, Password);
            manager.AddToRole(user.Id, role);
        }

    }
}