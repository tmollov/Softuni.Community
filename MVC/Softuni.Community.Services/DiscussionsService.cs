using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services
{
    public class DiscussionsService : IDiscussionsService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public DiscussionsService(SuCDbContext context,IMapper mapper, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        private bool IsUserLikedAnswer(int answerId, string username)
        {
            return this.context.UsersAnswerLikes.Any(x => x.User.UserName == username && x.AnswerId == answerId);
        }
        private bool IsUserDisLikedAnswer(int answerId, string username)
        {
            return this.context.UsersAnswerDislikes.Any(x => x.User.UserName == username && x.AnswerId == answerId);
        }

        public Answer RateAnswer(AnswerRatingBindingModel model)
        {
            var answer = this.context.Answers.FirstOrDefault(x => x.Id == model.AnswerId);
            var user = userService.GetUserByUserName(model.Username);
            
            if (model.Rating == 1)
            {
                if (!IsUserLikedAnswer(model.AnswerId,model.Username))
                {
                    var Answer = new UserAnswerLikes(answer.Id,user);
                    this.context.UsersAnswerLikes.Add(Answer);
                    if (IsUserDisLikedAnswer(model.AnswerId,model.Username))
                    {
                        var dislikedAnswer = this.context.UsersAnswerDislikes.FirstOrDefault(x=>x.User.UserName == model.Username && x.AnswerId== model.AnswerId);
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
                    var Answer = new UserAnswerDisLikes(answer.Id,user);
                    this.context.UsersAnswerDislikes.Add(Answer);

                    if (IsUserLikedAnswer(model.AnswerId,model.Username))
                    {
                        var likedAnswer = this.context.UsersAnswerLikes.FirstOrDefault(x=>x.User.UserName == model.Username && x.AnswerId== model.AnswerId);
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

        public Question RateQuestion(QuestionRatingBindingModel model)
        {
            var user = userService.GetUserByUserName(model.Username);
            
            if (model.Rating == 1)
            {
                if (!IsUserLikedQuestion(model.QuestionId,model.Username))
                {
                    var Question = new UserQuestionLikes(model.QuestionId,user);
                    this.context.UsersQuestionLikes.Add(Question);
                    if (IsUserDisLikedQuestion(model.QuestionId,model.Username))
                    {
                        var dislikedQuestion = this.context.UsersQuestionDislikes
                            .FirstOrDefault(x=>x.User.UserName == model.Username && x.QuestionId== model.QuestionId);
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
                    var Question = new UserQuestionDisLikes(model.QuestionId,user);
                    this.context.UsersQuestionDislikes.Add(Question);

                    if (IsUserLikedQuestion(model.QuestionId,model.Username))
                    {
                        var likedQuestion = this.context.UsersQuestionLikes
                            .FirstOrDefault(x=>x.User.UserName == model.Username && x.QuestionId == model.QuestionId);
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

        private bool IsUserDisLikedQuestion(int questionId, string username)
        {
            return this.context.UsersQuestionDislikes
                .Include(x=>x.User)
                .Any(x => x.User.UserName == username && x.QuestionId == questionId);
        }
        private bool IsUserLikedQuestion(int questionId, string username)
        {
            return this.context.UsersQuestionLikes
                .Include(x=>x.User)
                .Any(x => x.User.UserName == username && x.QuestionId == questionId);
        }

        public Answer DeleteAnswer(int AnswerId, int QuestionId)
        {
            var answer = this.context.Answers.FirstOrDefault(x => x.Id == AnswerId && x.QuestionId == QuestionId);
            this.context.Answers.Remove(answer);
            this.context.SaveChanges();
            return answer;
        }

        public ICollection<AnswerViewModel> GetAnswersViewModels(int id)
        {
            List<AnswerViewModel> vms = this.context.Answers
                .Include(x => x.Question)
                .Include(x => x.Publisher).ThenInclude(x => x.UserInfo)
                .Where(x=>x.QuestionId == id)
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
            vm.Tags = question.Tags.Select(x=>x.Name).ToList();
            return vm;
        }

        public Answer AddAnswer(string content, string username, int questionId)
        {
            var question = this.context.Questions.FirstOrDefault(x => x.Id == questionId);
            var publisher = userService.GetUserByUserName(username);
            var answer = new Answer(content, publisher, question);
            this.context.Answers.Add(answer);
            this.context.SaveChanges();

            return answer;
        }

        public Tag AddTag(string name, Question question)
        {
            var tag = new Tag(name, question);
            this.context.Tags.Add(tag);
            this.context.SaveChanges();
            return tag;
        }
        public ICollection<Tag> GenerateTagEntities(string tags, Question quest)
        {
            var tagList = tags.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim()).ToArray();
            List<Tag> tagsList = new List<Tag>();
            foreach (var item in tagList)
            {
                tagsList.Add(this.AddTag(item, quest));
            }
            return tagsList;
        }
        public Question AddQuestion(QuestionBingingModel model, string username)
        {
            var publisher = userService.GetUserByUserName(username);
            var question = new Question(model.Title, model.Content, model.Category, publisher);
            this.context.Questions.Add(question);
            this.context.SaveChanges();
            var tags = GenerateTagEntities(model.Tags, question);
            question.Tags = tags;
            this.context.SaveChanges();
            return question;
        }

        public bool IsUserLikesQuestion(string username)
        {
            var userId = userService.GetUserId(username);
            return this.context.UsersQuestionLikes.Any(x => x.UserId == userId);
        }

        public bool IsUserDisLikesQuestion(string username)
        {
            var userId = userService.GetUserId(username);
            return this.context.UsersQuestionDislikes.Any(x => x.UserId == userId);
        }

        public IList<int> GetUserLikedAnswersIdList(string username)
        {
            return this.context.UsersAnswerLikes.Include(x => x.User)
                .Where(x => x.User.UserName == username).Select(x=>x.AnswerId).ToList();
        }

        public IList<int> GetUserDisLikedAnswersIdList(string username)
        {
            return this.context.UsersAnswerDislikes.Include(x => x.User)
                .Where(x => x.User.UserName == username).Select(x=>x.AnswerId).ToList();
        }
    }
}
