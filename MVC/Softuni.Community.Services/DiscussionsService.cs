﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services
{
    public class DiscussionsService : IDiscussionsService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;

        public DiscussionsService(SuCDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool IsCurrentUserIsPublisherOfQuestion(int questionId, string userId)
        {
            return this.context.Questions
                       .FirstOrDefault(x => x.Id == questionId)
                       .PublisherId == userId;
        }

        public QuestionEditBindingModel GetQuestionEditBindingModel(int questionId)
        {
            var question = this.context.Questions
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == questionId);
            var bm = mapper.Map<QuestionEditBindingModel>(question);
            return bm;
        }

        public Question EditQuestion(QuestionEditBindingModel bindingModel)
        {
            var question = this.context.Questions.FirstOrDefault(x => x.Id == bindingModel.Id);
            question.Title = bindingModel.Title;
            question.Content = bindingModel.Content;
            question.Tags = this.UpdateTags(bindingModel.Tags, question);
            question.Category = bindingModel.Category;
            this.context.SaveChanges();
            return question;
        }

        public ICollection<Tag> UpdateTags(string tags, Question question)
        {
            if (tags == null)
                return new List<Tag>();

            if (!tags.Contains(";") && tags.Length > 0)
            {
                List<Tag> list = new List<Tag>();
                list.Add(this.AddTag(tags, question));
                return list;
            }

            var tagList = tags.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            var currentTags = this.context.Questions
                .Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == question.Id).Tags;

            List<Tag> newTagsList = new List<Tag>();
            foreach (var item in tagList)
            {
                if (currentTags.Any(x => x.Name == item))
                {
                    newTagsList.Add(currentTags.FirstOrDefault(x => x.Name == item));
                    continue;
                }
                newTagsList.Add(this.AddTag(item, question));
            }
            return newTagsList;
        }

        public Question DeleteQuestion(int questionId)
        {
            var question = this.context.Questions.FirstOrDefault(x => x.Id == questionId);
            if (question is null)
            {
                return null;
            }

            this.context.Questions.Remove(question);
            this.context.SaveChanges();
            return question;
        }

        public ICollection<AnswerViewModel> GetAnswersViewModels(int questionId)
        {
            List<AnswerViewModel> vms = this.context.Answers
                .Include(x => x.Question)
                .Include(x => x.Publisher).ThenInclude(x => x.UserInfo)
                .Where(x => x.QuestionId == questionId)
                .Select(x => mapper.Map<AnswerViewModel>(x)).ToList();
            return vms;
        }
        public ICollection<QuestionViewModel> GetQuestionViewModels()
        {
            List<QuestionViewModel> vms = this.context.Questions
                .Include(x => x.Answers)
                .Include(x => x.Tags)
                .Include(x => x.Publisher).ThenInclude(x => x.UserInfo)
                .Select(x => mapper.Map<QuestionViewModel>(x)).ToList();
            return vms;
        }
        public QuestionViewModel GetQuestionViewModel(int id)
        {
            Question question = this.context.Questions
                   .Include(x => x.Answers)
                   .Include(x => x.Tags)
                   .Include(x => x.Publisher)
                   .ThenInclude(x => x.UserInfo)
                   .FirstOrDefault(x => x.Id == id);

            QuestionViewModel vm = mapper.Map<QuestionViewModel>(question);
            vm.Tags = question.Tags.Select(x => x.Name).ToList();
            return vm;
        }

        public ICollection<MyQuestionViewModel> GetUserQuestions(string username)
        {
            var questions = this.context.Questions.Include(x => x.Publisher)
                .Where(x => x.Publisher.UserName == username)
                .Select(x => mapper.Map<MyQuestionViewModel>(x)).ToList();
            return questions;
        }
        public ICollection<MyAnswerViewModel> GetUserAnswers(string username)
        {
            var answers = this.context.Answers
                .Include(x => x.Publisher)
                .Include(x => x.Question)
                .Where(x => x.Publisher.UserName == username)
                .Select(x => mapper.Map<MyAnswerViewModel>(x)).ToList();
            return answers;
        }

        //Tested
        public Answer DeleteAnswer(int AnswerId, int QuestionId)
        {
            if (this.context.Answers.Any(x => x.Id == AnswerId && x.QuestionId == QuestionId))
            {
                var answer = this.context.Answers.FirstOrDefault(x => x.Id == AnswerId && x.QuestionId == QuestionId);
                this.context.Answers.Remove(answer);
                this.context.SaveChanges();
                return answer;
            }
            return null;
        }

        //Tested
        public Answer AddAnswer(string content, CustomUser publisher, int questionId)
        {
            if (this.context.Users.Any(x => x.Id == publisher.Id))
            {
                if (this.context.Questions.Any(x => x.Id == questionId))
                {

                    var question = this.context.Questions.FirstOrDefault(x => x.Id == questionId);
                    var answer = new Answer(content, publisher, question);
                    this.context.Answers.Add(answer);
                    this.context.SaveChanges();

                    return answer;
                }
            }
            return null;
        }

        //Tested
        public Answer RateAnswer(AnswerRatingBindingModel model, CustomUser user)
        {
            var answer = this.context.Answers.FirstOrDefault(x => x.Id == model.AnswerId);

            if (model.Rating == 1)
            {
                if (!IsUserLikedAnswer(model.AnswerId, model.Username))
                {
                    var Answer = new UserAnswerLikes(answer.Id, user);
                    this.context.UsersAnswerLikes.Add(Answer);
                    if (IsUserDisLikedAnswer(model.AnswerId, model.Username))
                    {
                        var dislikedAnswer = this.context.UsersAnswerDislikes.FirstOrDefault(x => x.User.UserName == model.Username && x.AnswerId == model.AnswerId);
                        this.context.UsersAnswerDislikes.Remove(dislikedAnswer);
                        this.context.SaveChanges();
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (!IsUserDisLikedAnswer(model.AnswerId, model.Username))
                {
                    var Answer = new UserAnswerDisLikes(answer.Id, user);
                    this.context.UsersAnswerDislikes.Add(Answer);

                    if (IsUserLikedAnswer(model.AnswerId, model.Username))
                    {
                        var likedAnswer = this.context.UsersAnswerLikes.FirstOrDefault(x => x.User.UserName == model.Username && x.AnswerId == model.AnswerId);
                        this.context.UsersAnswerLikes.Remove(likedAnswer);
                        this.context.SaveChanges();
                    }
                }
                else
                {
                    return null;
                }
            }

            answer.Rating += model.Rating;
            this.context.SaveChanges();
            return answer;
        }

        //Tested
        public Question RateQuestion(QuestionRatingBindingModel model, CustomUser user)
        {
            if (model.Rating == 1)
            {
                if (!IsUserLikedQuestion(model.QuestionId, model.Username))
                {
                    var Question = new UserQuestionLikes(model.QuestionId, user);
                    this.context.UsersQuestionLikes.Add(Question);
                    if (IsUserDisLikedQuestion(model.QuestionId, model.Username))
                    {
                        var dislikedQuestion = this.context.UsersQuestionDislikes
                            .FirstOrDefault(x => x.User.UserName == model.Username && x.QuestionId == model.QuestionId);
                        this.context.UsersQuestionDislikes.Remove(dislikedQuestion);
                        this.context.SaveChanges();
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (!IsUserDisLikedQuestion(model.QuestionId, model.Username))
                {
                    var Question = new UserQuestionDisLikes(model.QuestionId, user);
                    this.context.UsersQuestionDislikes.Add(Question);

                    if (IsUserLikedQuestion(model.QuestionId, model.Username))
                    {
                        var likedQuestion = this.context.UsersQuestionLikes
                            .FirstOrDefault(x => x.User.UserName == model.Username && x.QuestionId == model.QuestionId);
                        this.context.UsersQuestionLikes.Remove(likedQuestion);
                        this.context.SaveChanges();
                    }
                }
                else
                {
                    return null;
                }
            }

            var question = this.context.Questions.FirstOrDefault(x => x.Id == model.QuestionId);
            question.Rating += model.Rating;
            this.context.SaveChanges();
            return question;
        }

        //Tested
        public Question AddQuestion(QuestionBindingModel model, CustomUser publisher)
        {
            var question = new Question(model.Title, model.Content, model.Category, publisher);
            this.context.Questions.Add(question);
            this.context.SaveChanges();
            var tags = GenerateTagEntities(model.Tags, question);
            question.Tags = tags;
            this.context.SaveChanges();
            return question;
        }

        //Tested
        public Tag AddTag(string name, Question question)
        {
            var tag = new Tag(name, question);
            this.context.Tags.Add(tag);
            this.context.SaveChanges();
            return tag;
        }

        //Tested
        public ICollection<Tag> GenerateTagEntities(string tags, Question question)
        {
            if (tags == null)
                return new List<Tag>();

            if (!tags.Contains(";") && tags.Length > 0)
            {
                List<Tag> list = new List<Tag>();
                list.Add(this.AddTag(tags, question));
                return list;
            }
            var tagList = tags.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            List<Tag> tagsList = new List<Tag>();
            foreach (var item in tagList)
            {
                tagsList.Add(this.AddTag(item, question));
            }
            return tagsList;
        }


        public IList<QuestionViewModel> GetTopQuestions()
        {
            // best one joke from every category
            var questions = new List<QuestionViewModel>();
            foreach (string category in Enum.GetNames(typeof(Category)))
            {
                var question = this.context.Questions
                    .Where(x => x.Category == (Category)Enum.Parse(typeof(Category), category))
                    .OrderByDescending(x => x.Rating)
                    .FirstOrDefault();

                if (question != null)
                {
                    var vm = this.GetQuestionViewModel(question.Id);
                    questions.Add(vm);
                }
            }

            return questions;
        }

        //Tested
        public IList<int> GetUserLikedAnswersIdList(string username)
        {
            return this.context.UsersAnswerLikes.Include(x => x.User)
                .Where(x => x.User.UserName == username).Select(x => x.AnswerId).ToList();
        }
        //Tested
        public IList<int> GetUserDisLikedAnswersIdList(string username)
        {
            return this.context.UsersAnswerDislikes.Include(x => x.User)
                .Where(x => x.User.UserName == username).Select(x => x.AnswerId).ToList();
        }

        //Tested
        public bool IsUserLikedAnswer(int answerId, string username)
        {
            return this.context.UsersAnswerLikes.Any(x => x.User.UserName == username && x.AnswerId == answerId);
        }
        //Tested
        public bool IsUserDisLikedAnswer(int answerId, string username)
        {
            return this.context.UsersAnswerDislikes.Any(x => x.User.UserName == username && x.AnswerId == answerId);
        }

        //Tested
        public bool IsUserDisLikedQuestion(int questionId, string username)
        {
            return this.context.UsersQuestionDislikes
                .Include(x => x.User)
                .Any(x => x.User.UserName == username && x.QuestionId == questionId);
        }
        //Tested
        public bool IsUserLikedQuestion(int questionId, string username)
        {
            return this.context.UsersQuestionLikes
                .Include(x => x.User)
                .Any(x => x.User.UserName == username && x.QuestionId == questionId);
        }
    }
}
