using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IProblemsService
    {
        GameProblem AddProblem(AddProblemBindingModel bindingModel);
        AllProblemsViewModel GetAllProblemsViewModel();
        ProblemDetailsViewModel GetProblemDetails(int id);
    }
}
