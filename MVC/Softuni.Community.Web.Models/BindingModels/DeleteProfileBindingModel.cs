using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class DeleteProfileBindingModel
    {
        [Required(ErrorMessage = Error.EnterPasswordToDelete)]
        [StringLength(Required.PasswordMaxLength, ErrorMessage = Error.PasswordLength, MinimumLength = Required.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayName.EnterPassword)]
        public string Password { get; set; }
    }
}