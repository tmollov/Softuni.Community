using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.BindingModels.Interfaces
{
    public interface IProblemAddEdit
    {
        string Content { get; set; }

        string AnswerA { get; set; }

        string AnswerB { get; set; }

        string AnswerC { get; set; }

        string AnswerD { get; set; }

        ChoiceEnum RightAnswer { get; set; }
    }
}
