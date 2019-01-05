using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace Softuni.Community.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            return Redirect($"/Identity/Account/Login?ReturnUrl={ReturnUrl}");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}