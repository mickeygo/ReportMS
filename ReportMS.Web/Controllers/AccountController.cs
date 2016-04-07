using System.Web.Mvc;
using ReportMS.Web.Client.Models;
using ReportMS.Web.Client.Membership;

namespace ReportMS.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction("Login", new { returnUrl });

            var login = new Login();
            var loginResult = login.LogIn(model.UserName, model.Password, model.RememberMe);
            if (loginResult)
                return Url.IsLocalUrl(returnUrl) ? this.Redirect(returnUrl) : this.RedirectToHome();

            ModelState.AddModelError("", "The user name and password is not matched.");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            var login = new Login();
            login.LogOut();

            return this.RedirectToHome();
        }
    }
}