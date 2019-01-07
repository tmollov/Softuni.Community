﻿using System;
using AutoMapper;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web;
using Softuni.Community.Web.Models.BindingModels;
using Xunit;

namespace Softuni.Community.Services.Tests
{
    public class ProblemsServiceTests
    {
        public IMapper mapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        }).CreateMapper();

        [Fact]
        public void GetRightAnswerChoice_Must_Return_Choice_Depending_On_Index()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            // Act
            var result1 = service.GetRightAnswerChoice(0);
            var result2 = service.GetRightAnswerChoice(1);
            var result3 = service.GetRightAnswerChoice(2);
            var result4 = service.GetRightAnswerChoice(3);

            //Assert
            Assert.True(result1 == ChoiceEnum.A);
            Assert.True(result2 == ChoiceEnum.B);
            Assert.True(result3 == ChoiceEnum.C);
            Assert.True(result4 == ChoiceEnum.D);
        }

        [Fact]
        public void GetRightAnswerString_Must_Return_String_Of_Right_Choice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var rightA = GetTestAddProblemBM(ChoiceEnum.A);
            var rightB = GetTestAddProblemBM(ChoiceEnum.B);
            var rightC = GetTestAddProblemBM(ChoiceEnum.C);
            var rightD = GetTestAddProblemBM(ChoiceEnum.D);

            // Act
            var resultA = service.GetRightAnswerString(rightA);
            var resultB = service.GetRightAnswerString(rightB);
            var resultC = service.GetRightAnswerString(rightC);
            var resultD = service.GetRightAnswerString(rightD);
            //Assert
            Assert.True(resultA == Answer.A);
            Assert.True(resultB == Answer.B);
            Assert.True(resultC == Answer.C);
            Assert.True(resultD == Answer.D);
        }

        [Fact]
        public void AddProblem_Must_Return_Created_Problem()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var addProblemBM = GetTestAddProblemBM();
            // Act
            var result = service.AddProblem(addProblemBM);
            var testRightAnswer = service.GetRightAnswerString(addProblemBM);

            //Assert
            Assert.True(result.ProblemContent == addProblemBM.Content);
            Assert.True(result.RightAnswer == testRightAnswer);
            Assert.True(result.Choices.Count == 4);
        }

        [Fact]
        public void DeleteProblem_Must_Return_Deleted_Problem()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var addProblemBM = GetTestAddProblemBM();
            // Act
            var addedProblem = service.AddProblem(addProblemBM);
            var result = service.DeleteProblem(addedProblem.Id);

            //Assert
            Assert.True(result.Id == addedProblem.Id);
            Assert.True(result.ProblemContent == addedProblem.ProblemContent);
            Assert.True(result.RightAnswer == addedProblem.RightAnswer);
            Assert.True(result.Choices.Count == addedProblem.Choices.Count);
        }

        [Fact]
        public void GetEditProblemBindingModel_Must_Return_BindingModel_With_Given_Id()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var rightAnswer = ChoiceEnum.A;
            var addProblemBM = GetTestAddProblemBM(rightAnswer);

            // Act
            var addedProblem = service.AddProblem(addProblemBM);
            var result = service.GetEditProblemBindingModel(addedProblem.Id);

            //Assert
            Assert.True(result.Id == addedProblem.Id);
            Assert.True(result.Content == addedProblem.ProblemContent);
            Assert.True(result.RightAnswer == rightAnswer);
            Assert.True(result.AnswerA == Answer.A);
            Assert.True(result.AnswerB == Answer.B);
            Assert.True(result.AnswerC == Answer.C);
            Assert.True(result.AnswerD == Answer.D);
        }

        [Fact]
        public void EditProblem_Must_Return_Edited_Problem()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var rightAnswer = ChoiceEnum.A;
            var addProblemBM = GetTestAddProblemBM(rightAnswer);

            // Act
            var addedProblem = service.AddProblem(addProblemBM);
            var editProblemBM = service.GetEditProblemBindingModel(addedProblem.Id);
            editProblemBM.Content = "Testing...";
            editProblemBM.AnswerA = "nice";
            editProblemBM.AnswerB = "not this";
            editProblemBM.AnswerC = "THIS IS";
            editProblemBM.AnswerD = "NoNoNo";
            editProblemBM.RightAnswer = ChoiceEnum.C;

            var result = service.EditProblem(editProblemBM);
            var resultAnswer = service.GetRightAnswerString(editProblemBM);

            //Assert
            // Testing problem
            Assert.True(result.Id == editProblemBM.Id);
            Assert.True(result.ProblemContent == editProblemBM.Content);
            Assert.True(result.RightAnswer == resultAnswer);
            
            // Testing AnswerA
            Assert.True(result.Choices[0].Id == addedProblem.Choices[0].Id);
            Assert.True(result.Choices[0].Content == addedProblem.Choices[0].Content);
            Assert.True(result.Choices[0].GameProblemId == addedProblem.Id);

            // Testing AnswerB
            Assert.True(result.Choices[1].Id == addedProblem.Choices[1].Id);
            Assert.True(result.Choices[1].Content == addedProblem.Choices[1].Content);
            Assert.True(result.Choices[1].GameProblemId == addedProblem.Id);

            // Testing AnswerC
            Assert.True(result.Choices[2].Id == addedProblem.Choices[2].Id);
            Assert.True(result.Choices[2].Content == addedProblem.Choices[2].Content);
            Assert.True(result.Choices[2].GameProblemId == addedProblem.Id);

            // Testing AnswerD
            Assert.True(result.Choices[3].Id == addedProblem.Choices[3].Id);
            Assert.True(result.Choices[3].Content == addedProblem.Choices[3].Content);
            Assert.True(result.Choices[3].GameProblemId == addedProblem.Id);
        }

        [Fact]
        public void AddChoice_Must_Return_Created_Choice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var service = new ProblemsService(db, mapper);
            var addProblemBM = GetTestAddProblemBM();
            var choiceContent = GetTestContent;
            // Act
            var addedProblem = service.AddProblem(addProblemBM);
            var result = service.AddChoice(choiceContent,addedProblem);
            //Assert
            Assert.True(result.Content == choiceContent);
            Assert.True(result.GameProblemId == addedProblem.Id);
        }

        public AddProblemBindingModel GetTestAddProblemBM()
        {
            var bm = new AddProblemBindingModel()
            {
                Content = "Test question",
                RightAnswer = ChoiceEnum.B,
                AnswerA = "a",
                AnswerB = "b",
                AnswerC = "c",
                AnswerD = "d",
            };
            return bm;
        }
        public AddProblemBindingModel GetTestAddProblemBM(ChoiceEnum choice)
        {
            var bm = new AddProblemBindingModel()
            {
                Content = "Test question",
                RightAnswer = choice,
                AnswerA = "a",
                AnswerB = "b",
                AnswerC = "c",
                AnswerD = "d",
            };
            return bm;
        }

        public string GetTestContent = Guid.NewGuid().ToString();
    

        internal static class Answer
        {
            public static string A => "a";
            public static string B => "b";
            public static string C => "c";
            public static string D => "d";
        }
    }
}