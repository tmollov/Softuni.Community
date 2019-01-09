using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Models.BindingModels.Interfaces;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class AddProblemBindingModel : IProblemAddEdit
    {
        [Required(ErrorMessage = Error.ProblemContentRequired)]
        [Display(Name = DisplayName.ProblemContent)]
        public string Content { get; set; }

        [Required(ErrorMessage = Error.AnswerRequired)]
        [Display(Name = Answers.A)]
        public string AnswerA { get; set; }

        [Required(ErrorMessage = Error.AnswerRequired)]
        [Display(Name = Answers.B)]
        public string AnswerB { get; set; }

        [Required(ErrorMessage = Error.AnswerRequired)]
        [Display(Name = Answers.C)]
        public string AnswerC { get; set; }

        [Required(ErrorMessage = Error.AnswerRequired)]
        [Display(Name = Answers.D)]
        public string AnswerD { get; set; }

        [Required(ErrorMessage = Error.RightAnswerRequired)]
        [Display(Name = DisplayName.RightAnswer)]
        public ChoiceEnum RightAnswer { get; set; }
    }
}