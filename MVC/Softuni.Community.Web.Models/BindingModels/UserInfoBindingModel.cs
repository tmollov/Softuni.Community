using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class UserInfoBindingModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "What is you Birth Date ?")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Za-z]+$")]
        [Display(Name = "What is you First Name ?")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Za-z]+$")]
        [Display(Name = "What is you Last Name ?")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(300)]
        [Display(Name = "Share us more about you")]
        public string AboutMe { get; set; }
    }
}