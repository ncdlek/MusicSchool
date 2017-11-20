using MS.UI.Models;
using MS.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MS.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Login(LoginVM crendentials, string url)
        {
            if (ModelState.IsValid)
            {
                if (DataService.Service.userService.CheckCredentials(crendentials.Email, crendentials.Password))
                {
                    var currentUser = DataService.Service.userService.FindByEmail(crendentials.Email);

                    string cookie = $"{currentUser.Id}-{currentUser.Name} {currentUser.Surname}";

                    FormsAuthentication.SetAuthCookie(cookie, true);
                }
            }

            return Redirect(url);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}