using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Common;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class AddQuestionViewModel
    {
        [DataType(DataType.Text)]
        [MinLength(10)]
        [Display(Name = "Give it a Title.")]
        public string Title { get; set; }
        
        [DataType(DataType.MultilineText)]
        [MinLength(10)]
        [Display(Name = "What is your question ?")]
        public string Content { get; set; }

        [Display(Name = "Please seperate you tags with (semicolon)';' symbol.")]
        public string Tags { get; set; }
    }
}
