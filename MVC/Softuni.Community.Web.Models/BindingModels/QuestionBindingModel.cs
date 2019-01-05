using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class QuestionBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(10)]
        [Display(Name = "Give it a Title.")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [Display(Name = "What is your question ?")]
        public string Content { get; set; }

        [Display(Name = "Please separate you tags with (semicolon) ';' symbol.")]
        public string Tags { get; set; }

        [Display(Name = "Put it on one of these categories")]
        public Category Category { get;set;}
    }
}
