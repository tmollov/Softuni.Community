using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class ProfilesSettingsBindingModel
    {
        [DataType(DataType.Date)]
        [Display(Name = DisplayName.BirthDate)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Text)]
        [StringLength(Required.NameMax,ErrorMessage = Error.FirstNameLength,MinimumLength = Required.NameMin)]
        [RegularExpression(Required.FirstLastNameRegex)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(Required.NameMax,ErrorMessage = Error.FirstNameLength,MinimumLength = Required.NameMin)]
        [RegularExpression(Required.FirstLastNameRegex)]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(Required.AboutMeMax,ErrorMessage = Error.AboutMeLength,MinimumLength = Required.AboutMeMin)]
        [Display(Name = DisplayName.AboutMe)]
        public string AboutMe { get; set; }

        [DataType(DataType.Text)]
        [StringLength(Required.StateMax,ErrorMessage = Error.StateLength,MinimumLength = Required.StateMin)]
        public string State { get; set; }

        public string ProfilePictureUrl { get; set; }
        
        public IFormFile ProfilePicture { get; set; }
    }
}
