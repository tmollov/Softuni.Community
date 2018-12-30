using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class JokeRatingBindingModel
    {
        [Required]
        public int JokeId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
