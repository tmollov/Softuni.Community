using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Controllers;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area("Fun")]
    public class GamesController : BaseController
    {
        private const string Area = "Fun";
        private const string Controller = "Games";

        private readonly IProblemsService problemsService;

        public GamesController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        [Authorize]
        public IActionResult CodeWizard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageCodeWizard()
        {
            var vm = problemsService.GetAllProblemsViewModel();
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddProblem()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProblemDetails(int id)
        {
            var vm = problemsService.GetProblemDetails(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProblem(AddProblemBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.problemsService.AddProblem(bindingModel);
                return Redirect($"/{Area}/{Controller}/{nameof(ManageCodeWizard)}");
;            }
            return View(bindingModel);
        }

    }
}