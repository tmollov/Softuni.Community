using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class AnswerRatingBindingModel
    {
        [Required(ErrorMessage = Error.AnswerIDRequired)]
        [Range(0,int.MaxValue)]
        public int AnswerId { get; set; }

        [Required(ErrorMessage = Error.RatingRequired)]
        [Range(-1,1)]
        public int Rating { get; set; }

        [Required(ErrorMessage = Error.UsernameRequired)]
        [StringLength(Required.UsernameMaxLength, ErrorMessage = Error.UsernameLength, MinimumLength = Required.UsernameMinLength)]
        public string Username { get;set;}
    }
}