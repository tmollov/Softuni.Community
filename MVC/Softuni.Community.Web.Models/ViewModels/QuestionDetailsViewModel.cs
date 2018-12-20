using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public QuestionViewModel Question { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}