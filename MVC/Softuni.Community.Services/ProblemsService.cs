using System;
using System.Collections.Generic;
using System.Text;
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
    public class ProblemsService : IProblemsService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;

        public ProblemsService(SuCDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GameProblem AddProblem(AddProblemBindingModel bindingModel)
        {
            var problem = mapper.Map<GameProblem>(bindingModel);
            this.context.GameProblems.Add(problem);
            this.context.SaveChanges();
            problem.RightAnswer = GetRightAnswerContent(bindingModel);
            problem.Choices = this.GenerateChoices(bindingModel, problem);
            this.context.SaveChanges();
            return problem;
        }

        public IList<GameProblemViewModel> GetAllProblems()
        {
            var problems = new List<GameProblemViewModel>();
            foreach (var item in  this.context.GameProblems)
            {
                problems.Add(mapper.Map<GameProblemViewModel>(item));
            }
            return problems;
        }

        public ProblemDetailsViewModel GetProblemDetails(int id)
        {
            var problem = this.context.GameProblems
                .Include(x=>x.Choices)
                .FirstOrDefault(x=>x.Id == id);

            var vm = mapper.Map<ProblemDetailsViewModel>(problem);
            var index = vm.Answers.IndexOf(vm.RightAnswer);
            vm.Answers.RemoveAt(index);
            return vm;
        }

        public AllProblemsViewModel GetAllProblemsViewModel()
        {
            var vm = new AllProblemsViewModel();
            vm.GameProblems = this.GetAllProblems();
            return vm;
        }

        private string GetRightAnswerContent(AddProblemBindingModel bindingModel)
        {
            switch (bindingModel.RightAnswer)
            {
                case ChoiceEnum.A:
                    return bindingModel.AnswerA;
                case ChoiceEnum.B:
                    return bindingModel.AnswerB;
                case ChoiceEnum.C:
                    return bindingModel.AnswerC;
                case ChoiceEnum.D:
                    return bindingModel.AnswerD;
            }

            return null;
        }

        public IList<Choice> GenerateChoices(AddProblemBindingModel bindingModel, GameProblem problem)
        {
            var list = new List<Choice>();
            var a = AddChoice(bindingModel.AnswerA, problem);
            var b = AddChoice(bindingModel.AnswerB, problem);
            var c = AddChoice(bindingModel.AnswerC, problem);
            var d = AddChoice(bindingModel.AnswerD, problem);
            list.Add(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);
            return list;
        }

        public Choice AddChoice(string content, GameProblem problem)
        {
            var el = new Choice();
            el.Content = content;
            el.GameProblem = problem;
            this.context.Choices.Add(el);
            this.context.SaveChanges();
            return el;
        }

    }
}