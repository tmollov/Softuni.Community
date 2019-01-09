using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class QuestionBindingModel
    {
        [Required(ErrorMessage = Error.TitleRequired)]
        [StringLength(Required.TitleMaxLength, ErrorMessage = Error.TitleLength, MinimumLength = Required.TitleMinLength)]
        [DataType(DataType.Text)]
        [Display(Name = DisplayName.Title)]
        public string Title { get; set; }

        [Required(ErrorMessage = Error.QuestionCannotBeEmpty)]
        [StringLength(Required.QuestionMaxLength, ErrorMessage = Error.QuestionLength, MinimumLength = Required.QuestionMinLength)]
        [DataType(DataType.MultilineText)]
        [Display(Name = DisplayName.Question)]
        public string Content { get; set; }

        [Display(Name = DisplayName.Tags)]
        public string Tags { get; set; }

        [Required(ErrorMessage = Error.CategoryRequired)]
        [Display(Name = DisplayName.Categories)]
        public Category Category { get; set; }
    }
}