using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class DeleteAnswerBindingModel
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int AnswerId { get; set; }
    }
}