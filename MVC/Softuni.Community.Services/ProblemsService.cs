using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.BindingModels.Interfaces;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        public ProblemsService(SuCDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //Tested
        public GameProblem AddProblem(AddProblemBindingModel bindingModel)
        {
            var problem = mapper.Map<GameProblem>(bindingModel);
            this.context.GameProblems.Add(problem);
            this.context.SaveChanges();
            problem.RightAnswer = GetRightAnswerString(bindingModel);
            problem.Choices = this.AddChoices(bindingModel, problem);
            this.context.SaveChanges();
            return problem;
        }
        //Tested
        public GameProblem EditProblem(EditProblemBindingModel bindingModel)
        {
            var problem = this.context.GameProblems
                .Include(x => x.Choices)
                .FirstOrDefault(x => x.Id == bindingModel.Id);
            problem.ProblemContent = bindingModel.Content;
            problem.RightAnswer = GetRightAnswerString(bindingModel);
            problem.Choices = this.UpdateChoices(problem.Choices, bindingModel);
            this.context.SaveChanges();
            return problem;
        }
        //Tested
        public GameProblem DeleteProblem(int id)
        {
            var problem = this.context.GameProblems
                .Include(x=>x.Choices)
                .FirstOrDefault(x => x.Id == id);
            this.context.GameProblems.Remove(problem);
            this.context.SaveChanges();
            return problem;
        }
        //Tested
        public ProblemDetailsViewModel GetProblemDetails(int id)
        {
            var problem = this.context.GameProblems
                .Include(x => x.Choices)
                .FirstOrDefault(x => x.Id == id);

            var vm = mapper.Map<ProblemDetailsViewModel>(problem);
            var index = vm.Answers.IndexOf(vm.RightAnswer);
            vm.Answers.RemoveAt(index);
            return vm;
        }
        //Tested
        public IList<Choice> UpdateChoices(IList<Choice> choices, IProblemAddEdit bindingModel)
        {
            choices[0].Content = bindingModel.AnswerA;
            choices[1].Content = bindingModel.AnswerB;
            choices[2].Content = bindingModel.AnswerC;
            choices[3].Content = bindingModel.AnswerD;
            return choices;
        }

        //Tested
        public EditProblemBindingModel GetEditProblemBindingModel(int id)
        {
            var problem = this.context.GameProblems
                .Include(x => x.Choices)
                .FirstOrDefault(x => x.Id == id);

            var bm = mapper.Map<EditProblemBindingModel>(problem);
            var indexOfRightAnswer =
                problem.Choices
                    .IndexOf(
                        problem.Choices
                            .FirstOrDefault(x => x.Content == problem.RightAnswer)
                        );
            bm.RightAnswer = GetRightAnswerChoice(indexOfRightAnswer);
            return bm;
        }
        //Tested
        public IList<GameProblemViewModel> GetAllProblems()
        {
            var problems = new List<GameProblemViewModel>();
            foreach (var item in this.context.GameProblems)
            {
                problems.Add(mapper.Map<GameProblemViewModel>(item));
            }
            return problems;
        }

        //Tested
        public AllProblemsViewModel GetAllProblemsViewModel()
        {
            var vm = new AllProblemsViewModel
            {
                GameProblems = this.GetAllProblems()
            };
            return vm;
        }

        //Tested
        public ChoiceEnum GetRightAnswerChoice(int index)
        {
            switch (index)
            {
                case 0: return ChoiceEnum.A;
                case 1: return ChoiceEnum.B;
                case 2: return ChoiceEnum.C;
                case 3: return ChoiceEnum.D;
            }

            return ChoiceEnum.Error;
        }

        //Tested
        public string GetRightAnswerString(IProblemAddEdit bindingModel)
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

        //Tested
        public IList<Choice> AddChoices(IProblemAddEdit bindingModel, GameProblem problem)
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

        //Tested
        public Choice AddChoice(string content, GameProblem problem)
        {
            var el = new Choice
            {
                Content = content,
                GameProblem = problem
            };
            this.context.Choices.Add(el);
            this.context.SaveChanges();
            return el;
        }

        
        //Tested
        // For Api Controller
        public ProblemDetailsViewModel GetRandomProblem()
        {
            var problemIds = this.context.GameProblems.Select(x => x.Id).ToList();
            int min = problemIds.Min();
            int max = problemIds.Max();

            var randomNumber = RandomNumber(min, max);
            var problem = this.context.GameProblems
                .Include(x => x.Choices)
                .FirstOrDefault(x => x.Id == randomNumber);

            var vm = mapper.Map<ProblemDetailsViewModel>(problem);
            return vm;
        }
    }
}