using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web.Controllers
{
    public class DiscussionsController : BaseController
    {
        private readonly IDiscussionsService discussService;
        private readonly IMapper mapper;
        private readonly UserManager<CustomUser> userMgr;

        public DiscussionsController(IDiscussionsService discussService, IMapper mapper, UserManager<CustomUser> userMgr)
        {
            this.discussService = discussService;
            this.mapper = mapper;
            this.userMgr = userMgr;
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            var models = discussService.GetQuestionViewModels();
            var viewModel = new AllQuestionViewModel(models);

            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult DeleteAnswer(DeleteAnswerBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                var answer = discussService.DeleteAnswer(bindingModel.AnswerId, bindingModel.QuestionId, userId);
                if (answer == null)
                {
                    return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
                }
            }
            return Redirect($"/{Paths.Discussions}/{Actions.QuestionDetails}/{bindingModel.QuestionId}");
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult AddAnswer(AnswerBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                var answer = discussService.AddAnswer(bindingModel.Content, user, bindingModel.QuestionId);
                if (answer == null)
                {
                    return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
                }
            }
            var returnUrl = $"/{Paths.Discussions}/{Actions.QuestionDetails}/?{Query.Id}={bindingModel.QuestionId}";
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult QuestionDetails(string id)
        {
            var parms = id.Split("#").Select(int.Parse).ToArray();
            var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
            var vm = discussService.GetQuestionDetailsVM(parms[0],userId);
            if (vm != null)
            {
                return this.View(vm);
            }

            return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
        }

        [Authorize]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddQuestion(QuestionBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                discussService.AddQuestion(bindingModel, user);
                return RedirectToAction(Actions.All, Paths.Discussions);
            }
            return View(bindingModel);
        }

        [Authorize]
        public IActionResult EditQuestion(int questionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                var bm = discussService.GetQuestionEditBindingModel(questionId, userId);
                if (bm == null)
                {
                    return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
                }

                return this.View(bm);
            }
            return Redirect($"/{Paths.Discussions}/{Actions.QuestionDetails}/?Id={questionId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditQuestion(QuestionEditBindingModel bindingModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                var bm = discussService.EditQuestion(bindingModel, userId);
            }
            return Redirect($"/{Paths.Discussions}/{Actions.QuestionDetails}/?Id={bindingModel.Id}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteQuestion(int questionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userMgr.FindByNameAsync(User.Identity.Name).Result.Id;
                discussService.DeleteQuestion(questionId, userId);
                return RedirectToAction(Actions.All, Paths.Discussions);
            }

            return RedirectToAction(Actions.SomethingWentWrong, Paths.Error);
        }

    }
}