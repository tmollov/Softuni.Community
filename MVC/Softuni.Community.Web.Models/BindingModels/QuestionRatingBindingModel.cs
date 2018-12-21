using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class QuestionRatingBindingModel
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Username { get;set;}
    }
}
