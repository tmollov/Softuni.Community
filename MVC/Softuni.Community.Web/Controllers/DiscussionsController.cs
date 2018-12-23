﻿using System;
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

        [Authorize]
        [HttpPost]
        public IActionResult DeleteAnswer(DeleteAnswerBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var answer = discussService.DeleteAnswer(bindingModel.AnswerId, bindingModel.QuestionId);
                if (answer == null)
                {
                    throw new Exception("Answer didnt found");
                }
            }
            return Redirect($"/Discussions/QuestionDetails/{bindingModel.QuestionId}");
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult AddAnswer(AnswerBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                var answer = discussService.AddAnswer(bindingModel.Content,user,bindingModel.QuestionId);
                if (answer == null)
                {
                    throw new Exception("Cannot add answer");
                }
            }
            return Redirect($"/Discussions/QuestionDetails/{bindingModel.QuestionId}");
        }

        [AllowAnonymous]
        public IActionResult QuestionDetails(int id)
        {
            var questionViewModel = discussService.GetQuestionViewModel(id);
            var answersViewModels = discussService.GetAnswersViewModels(id);
            var isUserLikeQuestion = discussService.IsUserLikedQuestion(questionViewModel.QuestionId,User.Identity.Name);
            var isUserDisLikeQuestion = discussService.IsUserDisLikedQuestion(questionViewModel.QuestionId,User.Identity.Name);
            var userLikedAnswers = discussService.GetUserLikedAnswersIdList(User.Identity.Name);
            var userDisLikedAnswers = discussService.GetUserDisLikedAnswersIdList(User.Identity.Name);
            var res = new QuestionDetailsViewModel()
            {
                Question = questionViewModel,
                Answers = answersViewModels,
                IsUserLikeQuestion = isUserLikeQuestion,
                IsUserDisLikeQuestion = isUserDisLikeQuestion,
                ListOfLikedAnswers = userLikedAnswers,
                ListOfDisLikedAnswers = userDisLikedAnswers
            };
            return this.View(res);
        }

        [Authorize]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddQuestion(QuestionBingingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = userMgr.FindByNameAsync(User.Identity.Name).Result;
                discussService.AddQuestion(bindingModel, user);
                return RedirectToAction(ActionsConts.AllDiscussions, ControllersConts.Discussions);
            }
            return View(bindingModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }

    }
}