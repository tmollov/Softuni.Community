using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<MyQuestionViewModel> MyQuestions { get; set; }
        public ICollection<MyAnswerViewModel> MyAnswers { get; set; }
        public ICollection<QuestionViewModel> MyScores { get; set; }
        public MyProfileViewModel MyProfile { get; set; }
    }
}