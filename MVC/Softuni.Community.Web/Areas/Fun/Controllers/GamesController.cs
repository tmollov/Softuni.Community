using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area("Fun")]
    public class GamesController : Controller
    {
        public IActionResult CodeWizard()
        {
            return View();
        }
    }
}