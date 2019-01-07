﻿using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Models.BindingModels.Interfaces;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class AddProblemBindingModel : IProblemAddEdit
    {
        [Required]
        [Display(Name = "Fill Problem description")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "A")]
        public string AnswerA { get; set; }

        [Required]
        [Display(Name = "B")]
        public string AnswerB { get; set; }

        [Required]
        [Display(Name = "C")]
        public string AnswerC { get; set; }

        [Required]
        [Display(Name = "D")]
        public string AnswerD { get; set; }

        [Required]
        [Display(Name = "What is the Right Answer for this problem?")]
        public ChoiceEnum RightAnswer { get; set; }

    }
}