using System.ComponentModel.DataAnnotations;
using Softuni.Community.Web.Common;

namespace Softuni.Community.Web.Models.BindingModels
{
    public class DeleteAnswerBindingModel
    {
        [Required(ErrorMessage = Error.QuestionIDRequired)]
        [Range(0,int.MaxValue)]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = Error.AnswerIDRequired)]
        [Range(0,int.MaxValue)]
        public int AnswerId { get; set; }
    }
}