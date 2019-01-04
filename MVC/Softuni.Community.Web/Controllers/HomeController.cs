using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IJokesService jokeService;
        private readonly IDiscussionsService discussionsService;
        private readonly UserManager<CustomUser> userMgr;

        public HomeController(IJokesService jokeService, IDiscussionsService discussionsService,  UserManager<CustomUser> userMgr)
        {
            this.jokeService = jokeService;
            this.discussionsService = discussionsService;
            this.userMgr = userMgr;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                var vm = new IndexViewModel();
                vm.Jokes = jokeService.GetTopJokes(userId);
                return View(vm);
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Fun()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
