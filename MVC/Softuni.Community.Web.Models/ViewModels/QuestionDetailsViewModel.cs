using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public QuestionViewModel Question { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public bool IsUserLikeQuestion { get; set; }

        public bool IsUserDisLikeQuestion { get; set; }

        public IList<int> ListOfLikedAnswers { get; set; }

        public IList<int> ListOfDisLikedAnswers { get; set; }
    }
}