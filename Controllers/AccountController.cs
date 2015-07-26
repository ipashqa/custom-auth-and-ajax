using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LabProject.Models.Auth;
using LabProject.DAL;
using LabProject.DAL.Security;
using System.Web.Security;
using System.Data.Entity;

namespace LabProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        LabProjectContext context = new LabProjectContext();        

        public ActionResult Login()
        {
            if(Request.IsAjaxRequest())
                return PartialView("_Login");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users
                    .Where(u => u.Email == model.Email && u.Password == model.Password)
                    .FirstOrDefault();

                if (user != null)
                {
                    string encTicket = AuthenticationHelper.GetAuthTicketAsString(user, model.RememberMe);

                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    if (Request.IsAjaxRequest())
                        return PartialView("_LoginSuccess", model.Email);
                    else
                        return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            if (Request.IsAjaxRequest())
                return PartialView("_Login", model);
            else
                return View(model);
        }

        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var anyUser = context.Users.Any(user => user.Email == model.Email);

                if (anyUser)
                    ModelState.AddModelError("Email", "This email is already registered");
                else
                {
                    User user = new User()
                    {
                        Email = model.Email,
                        Password = model.Password,
                        RoleId = 2, //user
                        CreationDate = DateTime.Now
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    user = context.Users
                        .Where(u => u.Email == model.Email)
                        .Include(u => u.Role)
                        .FirstOrDefault();

                    string encTicket = AuthenticationHelper.GetAuthTicketAsString(user, false);

                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
