using System.Collections.Generic;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IDiscussionsService
    {
        ICollection<AnswerViewModel> GetAnswersViewModels(int id);
        QuestionViewModel GetQuestionViewModel(int id);
        ICollection<QuestionViewModel> GetQuestionViewModels();
        Answer AddAnswer(string content, string username, int questionId);
        Question AddQuestion(QuestionBingingModel model, string username);
        Tag AddTag(string name, Question question);
    }
}