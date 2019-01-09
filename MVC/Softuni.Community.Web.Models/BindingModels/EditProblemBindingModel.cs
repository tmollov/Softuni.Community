using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Common;
using Softuni.Community.Web.Models.BindingModels.Interfaces;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class EditProblemBindingModel : IProblemAddEdit
    {
        
        [Required(ErrorMessage = Error.IdRequired)]
        [Range(0,int.MaxValue)]
        public int Id { get; set; }

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