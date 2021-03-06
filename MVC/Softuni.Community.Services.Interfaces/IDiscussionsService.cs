﻿using System.Collections.Generic;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IDiscussionsService
    {
        QuestionEditBindingModel GetQuestionEditBindingModel(int questionId,string publisherId);
        Question EditQuestion(QuestionEditBindingModel bindingModel, string publisherId);
        Question DeleteQuestion(int questionId, string publisherId);
        
        IList<QuestionViewModel> GetTopQuestions();
        IList<int> GetUserLikedAnswersIdList(string publisherId);
        IList<int> GetUserDisLikedAnswersIdList(string userId);
        ICollection<MyQuestionViewModel> GetUserQuestionsVM(string userId);
        ICollection<MyAnswerViewModel> GetUserAnswersVM(string userId);
        QuestionDetailsViewModel GetQuestionDetailsVM(int questionId, string userId);

        bool IsUserDisLikedQuestion(int questionId, string userId);
        bool IsUserLikedQuestion(int questionId, string userId);

        ICollection<AnswerViewModel> GetAnswersViewModels(int questionId);
        QuestionViewModel GetQuestionViewModel(int id);
        ICollection<QuestionViewModel> GetQuestionViewModels();

        Answer AddAnswer(string content, CustomUser publisher, int questionId);
        Answer DeleteAnswer(int AnswerId, int QuestionId, string publisherId);
        Answer RateAnswer(AnswerRatingBindingModel model, CustomUser user);

        Question AddQuestion(QuestionBindingModel model, CustomUser user);
        Question RateQuestion(QuestionRatingBindingModel model, CustomUser user);

        Tag AddTag(string name, Question question);
    }
}