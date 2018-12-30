﻿using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Web.Controllers;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area("Fun")]
    public class GamesController : BaseController
    {
        public IActionResult CodeWizard()
        {
            return View();
        }
    }
}