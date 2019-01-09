using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class RegisterBindingModel
    {
        [Required(ErrorMessage = Error.EmailRequired)]
        [EmailAddress]
        [Display(Name = DisplayName.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = Error.UsernameRequired)]
        [StringLength(Required.UsernameMaxLength, ErrorMessage = Error.UsernameLength , MinimumLength = Required.UsernameMinLength)]
        [DataType(DataType.Text)]
        [Display(Name = DisplayName.Username)]
        public string UserName { get; set; }

        [Required(ErrorMessage = Error.PasswordRequired)]
        [StringLength(Required.PasswordMaxLength, ErrorMessage = Error.PasswordLength, MinimumLength = Required.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayName.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = Error.ConfirmPasswordRequired)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayName.ConfirmPassword)]
        [Compare(DisplayName.Password, ErrorMessage = Error.PasswordsDoesntMatch)]
        public string ConfirmPassword { get; set; }
    }
}