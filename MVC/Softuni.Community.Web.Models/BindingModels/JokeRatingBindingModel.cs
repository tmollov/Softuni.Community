using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class JokeRatingBindingModel
    {
        [Required(ErrorMessage = Error.JokeIDRequired)]
        [Range(Required.IdMin,Required.IdMax)]
        public int JokeId { get; set; }

        [Required(ErrorMessage = Error.RatingRequired)]
        [Range(Required.RatingMin,Required.RatingMax)]
        public int Rating { get; set; }

        [Required(ErrorMessage = Error.UsernameRequired)]
        [StringLength(Required.UsernameMaxLength, ErrorMessage = Error.UsernameLength , MinimumLength = Required.UsernameMinLength)]
        public string Username { get; set; }
    }
}