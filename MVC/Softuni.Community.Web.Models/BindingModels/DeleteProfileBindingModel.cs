using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class DeleteProfileBindingModel
    {
        [Required(ErrorMessage = Error.PasswordRequired)]
        [StringLength(Required.PasswordMaxLength, ErrorMessage = Error.PasswordLength, MinimumLength = Required.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayName.Password)]
        public string Password { get; set; }
    }
}