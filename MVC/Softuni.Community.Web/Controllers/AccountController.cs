using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var redirectUrl = $"/{Area.Identity}/{Paths.Account}/{Actions.Login}?{Query.ReturnUrl}={returnUrl}";
            return Redirect(redirectUrl);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}