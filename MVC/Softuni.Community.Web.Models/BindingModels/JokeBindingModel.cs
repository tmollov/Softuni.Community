using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class JokeBindingModel
    {
        [Required]
        [MinLength(20)]
        [Display(Name = "Joke")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Put it on one of these categories:")]
        public JokeCategory Category { get; set; }
    }
}
