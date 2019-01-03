﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Controllers;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area("Fun")]
    [AllowAnonymous]
    public class JokesController : BaseController
    {
        private readonly IJokesService jokesService;
        private readonly UserManager<CustomUser> userMgr;

        public JokesController(IJokesService jokesService, UserManager<CustomUser> userMgr)
        {
            this.jokesService = jokesService;
            this.userMgr = userMgr;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(JokeBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                this.jokesService.AddJoke(bindingModel,user);
                return Redirect("/Fun/Jokes/All");
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var vm = jokesService.GetJoke<JokeEditBindingModel>(id);
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(JokeEditBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                jokesService.EditJoke(bindingModel);
                return Redirect($"/Fun/Jokes/Details/?id={bindingModel.Id}");
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var deletedJoke = jokesService.DeleteJoke(id);

            return Redirect("/Fun/Jokes/All");
        }


        public IActionResult All()
        {
            var viewModel = jokesService.GetAllJokes();
            if (User.Identity.Name != null)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                viewModel.UserLikedJokes = jokesService.GetUserLikedJokesIdList(userId);
                viewModel.UserDislikedJokes = jokesService.GetUserDisikedJokesIdList(userId);
            }
            return View(viewModel);
        }
        
        public IActionResult Details(int id)
        {
            var vm = jokesService.GetJoke<JokeViewModel>(id);
            var userid = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
            vm.IsUserLikedJoke = jokesService.IsUserLikedJoke(vm.Id, userid);
            vm.IsUserDislikedJoke = jokesService.IsUserDisLikedJoke(vm.Id, userid);
            return View(vm);
        }
    }
}