using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class AnswerBindingModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public int QuestionId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}