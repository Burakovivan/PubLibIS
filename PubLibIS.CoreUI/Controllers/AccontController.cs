using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubLibIS.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PubLibIS.CoreUI.Controllers
{
    [Route("api/[controller]")]
    public class AccontController : Controller
    {
    private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();
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
