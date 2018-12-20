using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
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
    }
}