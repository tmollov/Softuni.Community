using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Controllers;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web.Areas.Fun.Controllers
{
    [Area(Area.Fun)]
    public class GamesController : BaseController
    {
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

        [Authorize(Roles = Role.Admin)]
        public IActionResult ManageCodeWizard()
        {
            var vm = problemsService.GetAllProblemsViewModel();
            return View(vm);
        }

        [Authorize(Roles = Role.Admin)]
        public IActionResult AddProblem()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult AddProblem(AddProblemBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                this.problemsService.AddProblem(bindingModel);
                return Redirect($"/{Area.Fun}/{Paths.Games}/{Actions.ManageCodeWizard}");
                ;            }
            return View(bindingModel);
        }

        [Authorize(Roles = Role.Admin)]
        public IActionResult ProblemDetails(int id)
        {
            var vm = problemsService.GetProblemDetails(id);
            return View(vm);
        }

        [Authorize(Roles = Role.Admin)]
        public IActionResult EditProblem(int id)
        {
            var bm = problemsService.GetEditProblemBindingModel(id);
            return View(bm);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public IActionResult EditProblem(EditProblemBindingModel bindingModel)
        {
            var bm = problemsService.EditProblem(bindingModel);
            var redirectUrl = $"/{Area.Fun}/{Paths.Games}/{Actions.ProblemDetails}?{Query.Id}={bm.Id}";
            return Redirect(redirectUrl);
        }

        [Authorize(Roles = Role.Admin)]
        public IActionResult DeleteProblem(int id)
        {
            var bm = problemsService.DeleteProblem(id);
            return Redirect($"/{Area.Fun}/{Paths.Games}/{Actions.ManageCodeWizard}");
        }
    }
}