using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Required(ErrorMessage = Error.EmailRequired)]
        [EmailAddress]
        [Display(Name = DisplayName.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = Error.PasswordRequired)]
        [StringLength(Required.PasswordMaxLength, ErrorMessage = Error.PasswordLength, MinimumLength = Required.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayName.Password)]
        public string Password { get; set; }

        [Display(Name = DisplayName.RememberMe)]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}