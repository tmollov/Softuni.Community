using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Web.Controllers;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area("Fun")]
    public class MemesController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}