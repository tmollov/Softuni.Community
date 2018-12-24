﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class ProfilesSettingsBindingModel
    {
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Text)]
        [MinLength(4)]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [MinLength(4)]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(300)]
        public string AboutMe { get; set; }

        public string ProfilePictureUrl { get; set; }
        
        public IFormFile ProfilePicture { get; set; }
    }
}