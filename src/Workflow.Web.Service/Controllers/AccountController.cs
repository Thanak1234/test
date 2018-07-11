using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net;
using System.Web.Security;
using Workflow.Web.Service.Models;
using Workflow.Web.Service.Controllers.Admin;
using System.Configuration;
using System.IdentityModel.Services;

namespace Workflow.Web.Service.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController() { }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            //ViewBag.Version = new ConfigurationController().GetVersion();
            //ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }
        
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(NgFormAuthentication.ApplicationCookie);
            return RedirectToAction("Index");
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.UserName))
                {
                    ViewBag.ErrorMessage = "Please input your user name and password!";
                }
                else if (string.IsNullOrEmpty(model.UserName))
                {
                    ViewBag.ErrorMessage = "Please input your user name!";
                }
                else if (string.IsNullOrEmpty(model.Password))
                {
                    ViewBag.ErrorMessage = "Please input your password!";
                }
                return View(model);
            }

            if(model.UserName.ToLower().Contains("nagaworld"))
            {
                model.UserName = model.UserName.RegexReplace(@"nagaworld\\|\@nagaworld.com|\@nagaworld", string.Empty);
            }

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.UserName, model.Password);

            if (authenticationResult.IsSuccess)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }
    }
}
