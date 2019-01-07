using System.Collections.Generic;
using Softuni.Community.Data.Models;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.BindingModels.Interfaces;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IProblemsService
    {
        GameProblem AddProblem(AddProblemBindingModel bindingModel);
        GameProblem DeleteProblem(int id);
        GameProblem EditProblem(EditProblemBindingModel bindingModel);

        AllProblemsViewModel GetAllProblemsViewModel();
        ProblemDetailsViewModel GetProblemDetails(int id);
        ProblemDetailsViewModel GetRandomProblem();
        EditProblemBindingModel GetEditProblemBindingModel(int id);
        Choice AddChoice(string content, GameProblem problem);

        IList<GameProblemViewModel> GetAllProblems();
        IList<Choice> UpdateChoices(IList<Choice> choices, IProblemAddEdit bindingModel);
        IList<Choice> AddChoices(IProblemAddEdit bindingModel, GameProblem problem);
        ChoiceEnum GetRightAnswerChoice(int index);

        string GetRightAnswerString(IProblemAddEdit bindingModel);
    }
}