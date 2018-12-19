using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Softuni.Community.Web.Controllers
{
    [AllowAnonymous]
    public class DiscussionsController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }



    }
}