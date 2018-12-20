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
    }
}
