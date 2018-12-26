using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class MyProfileViewModel
    {
        public DateTime BirthDate { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string AboutMe { get; set; }

        public string ProfilePictureUrl { get; set; }

    }
}