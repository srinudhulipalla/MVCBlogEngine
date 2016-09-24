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
using MVCBlogEngine.Models;
using System.Web.Security;
using MVCBlogEngine.Common;

namespace MVCBlogEngine.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
                
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = BlogEngineDB.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                return View(model);
            }
            else
            {
                Current.UserId = user.UserId;
                string[] userRoles = user.UserRoles.Select(r => r.Role.RoleName).ToArray();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, string.Join(",", userRoles), FormsAuthentication.FormsCookiePath);

                string cookie = FormsAuthentication.Encrypt(ticket);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookie);
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                return RedirectToAction("Index", "Dashboard");
            }

           
        }

       

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

       

       


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

      

       
    }
}