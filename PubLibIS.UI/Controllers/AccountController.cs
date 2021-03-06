﻿using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PubLibIS.BLL.Services;
using PubLibIS.UI.Util;
using PubLibIS.ViewModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PubLibIS.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserService UserService => HttpContext.GetOwinContext().GetUserManager<UserService>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        public ActionResult SwitchConnection()
        {
            AuthenticationManager.SignOut();
            ConnectionStringResolver.SwitchConnection();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = await UserService.Authenticate(model);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
             if (ModelState.IsValid)
            {
                try
                {
                    await UserService.Create(model);
                    await Login(new LoginModel { Email = model.Email, Password = model.Password });
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, e.Message);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}