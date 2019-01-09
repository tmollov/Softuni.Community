using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Controllers;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area(Area.Fun)]
    public class JokesController : BaseController
    {
        private readonly IJokesService jokesService;
        private readonly UserManager<CustomUser> userMgr;

        public JokesController(IJokesService jokesService, UserManager<CustomUser> userMgr)
        {
            this.jokesService = jokesService;
            this.userMgr = userMgr;
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Add(JokeBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                this.jokesService.AddJoke(bindingModel,user);
                return Redirect($"/{Area.Fun}/{Paths.Jokes}/{Actions.All}");
            }
            return View(bindingModel);
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(int id)
        {
            var vm = jokesService.GetJoke<JokeEditBindingModel>(id);
            return View(vm);
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(JokeEditBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                jokesService.EditJoke(bindingModel);
                var redirectUrl = $"/{Area.Fun}/{Paths.Jokes}/{Actions.JokeDetails}/?id={bindingModel.Id}";
                return Redirect(redirectUrl);
            }
            return View(bindingModel);
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {
            var deletedJoke = jokesService.DeleteJoke(id);
            var redirectUrl = $"/{Area.Fun}/{Paths.Jokes}/{Actions.All}";
            return Redirect(redirectUrl);
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var viewModel = jokesService.GetAllJokes();
            if (User.Identity.Name != null)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                viewModel.UserLikedJokes = jokesService.GetUserLikedJokesIdList(userId);
                viewModel.UserDislikedJokes = jokesService.GetUserDislikedJokesIdList(userId);
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var vm = jokesService.GetJoke<JokeViewModel>(id);
            if (User.Identity.Name != null)
            {
                var userid = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                vm.IsUserLikedJoke = jokesService.IsUserLikedJoke(vm.Id, userid);
                vm.IsUserDislikedJoke = jokesService.IsUserDisLikedJoke(vm.Id, userid);
            }
            return View(vm);
        }
    }
}