using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class JokeEditBindingModel
    {
        [Required(ErrorMessage = Error.JokeIDRequired)]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = Error.JokeContentRequired)]
        [StringLength(Required.JokeContentMaxLength,ErrorMessage = Error.JokeLength,MinimumLength = Required.JokeContentMinLength)]
        [Display(Name = DisplayName.Joke)]
        public string Content { get; set; }

        [Required(ErrorMessage = Error.CategoryRequired)]
        [Display(Name = DisplayName.Categories)]
        public JokeCategory Category { get; set; }
    }
}