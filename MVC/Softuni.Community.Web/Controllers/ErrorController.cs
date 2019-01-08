using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Softuni.Community.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult SomethingWentWrong()
        {
            return View();
        }
    }
}