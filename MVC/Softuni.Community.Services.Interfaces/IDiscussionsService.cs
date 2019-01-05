using System.Collections.Generic;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IDiscussionsService
    {
        QuestionEditBindingModel GetQuestionEditBindingModel(int questionId);
        Question EditQuestion(QuestionEditBindingModel bindingModel);
        Question DeleteQuestion(int questionId);

        IList<QuestionViewModel> GetTopQuestions();
        IList<int> GetUserLikedAnswersIdList(string username);
        IList<int> GetUserDisLikedAnswersIdList(string username);
        ICollection<MyQuestionViewModel> GetUserQuestions(string username);
        ICollection<MyAnswerViewModel> GetUserAnswers(string username);

        bool IsUserDisLikedQuestion(int questionId, string username);
        bool IsUserLikedQuestion(int questionId, string username);
        
        ICollection<AnswerViewModel> GetAnswersViewModels(int questionId);
        QuestionViewModel GetQuestionViewModel(int id);
        ICollection<QuestionViewModel> GetQuestionViewModels();

        Answer AddAnswer(string content, CustomUser publisher, int questionId);
        Answer DeleteAnswer(int AnswerId, int QuestionId);
        Answer RateAnswer(AnswerRatingBindingModel model, CustomUser user);

        Question AddQuestion(QuestionBindingModel model, CustomUser user);
        Question RateQuestion(QuestionRatingBindingModel model, CustomUser user);

        Tag AddTag(string name, Question question);
    }
}