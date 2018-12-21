using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class AnswerRatingBindingModel
    {
        [Required]
        public int AnswerId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Username { get;set;}
    }
}