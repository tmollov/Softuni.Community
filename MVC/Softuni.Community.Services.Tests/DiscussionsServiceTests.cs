﻿using System.Linq;
using AutoMapper;
using Softuni.Community.Data.Models;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web;
using Softuni.Community.Web.Models.BindingModels;
using Xunit;

namespace Softuni.Community.Services.Tests
{
    public class DiscussionsServiceTests
    {
        public IMapper mapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        }).CreateMapper();

        /// <summary>
        /// Questions Test
        /// </summary>
        [Fact]
        public void AddQuestion_Must_Return_Created_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var result = discussionsService.AddQuestion(testQBM, testUser);
            //Assert
            Assert.True(result.PublisherId == testUser.Id);
            Assert.True(result.Title == testQBM.Title);
            Assert.True(result.Content == testQBM.Content);
            Assert.True(result.Category == testQBM.Category);
            Assert.True(result.Tags.Count == 2);
        }
        [Fact]
        public void AddTag_Must_Return_Created_Tag()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var question = discussionsService.AddQuestion(testQBM, testUser);
            var tag = discussionsService.AddTag("tag", question);
            //Assert
            Assert.True(tag.Name == "tag");
            Assert.True(tag.QuestionId == question.Id);
            Assert.True(question.Tags.Count == 3);


        }
        [Fact]
        public void GenerateTagEntities_Must_Return_List_Of_Created_Tags()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            var testTags = " newTag myTag";
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var question = discussionsService.AddQuestion(testQBM, testUser);
            var tags = discussionsService.GenerateTagEntities(testTags, question).ToArray();
            //Assert
            Assert.True(tags.Length == 2);
            Assert.True(tags[0].Name == "newTag");
            Assert.True(tags[0].QuestionId == question.Id);

            Assert.True(tags[1].Name == "myTag");
            Assert.True(tags[1].QuestionId == question.Id);
        }
        [Fact]
        public void RateQuestion_Must_Return_Updated_Question_RatedUp()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingUp(addedQuestion);
            var ratedQuestion = discussionsService.RateQuestion(questionRatingBindingModel, testUser);

            //Assert
            Assert.True(ratedQuestion.Id == addedQuestion.Id);
            Assert.True(ratedQuestion.Rating == 1);
        }
        [Fact]
        public void RateQuestion_Must_Return_NULL_If_RatedUp_Twice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingUp(addedQuestion);
            discussionsService.RateQuestion(questionRatingBindingModel, testUser);
            var thisMustBeNull = discussionsService.RateQuestion(questionRatingBindingModel, testUser);

            //Assert
            Assert.True(thisMustBeNull == null);
        }
        [Fact]
        public void RateQuestion_Must_Return_Updated_Question_RatedDown()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingDown(addedQuestion);
            var ratedQuestion = discussionsService.RateQuestion(questionRatingBindingModel, testUser);

            //Assert
            Assert.True(ratedQuestion.Id == addedQuestion.Id);
            Assert.True(ratedQuestion.Rating == -1);
        }
        [Fact]
        public void RateQuestion_Must_Return_NULL_If_RatedDown_Twice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingDown(addedQuestion);
            discussionsService.RateQuestion(questionRatingBindingModel, testUser);
            var thisMustBeNull = discussionsService.RateQuestion(questionRatingBindingModel, testUser);

            //Assert
            Assert.True(thisMustBeNull == null);
        }

        /// <summary>
        /// Answer Tests
        /// </summary>
        [Fact]
        public void RateAnswer_Must_Return_Updated_Answer_RatedUp()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingUp(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);

            //Assert
            Assert.True(ratedAnswer != null);
            Assert.True(ratedAnswer.Rating == 1);
            Assert.True(ratedAnswer.QuestionId == addedQuestion.Id);
            Assert.True(ratedAnswer.PublisherId == testUser.Id);
            Assert.True(ratedAnswer.Content == answer.Content);
        }
        [Fact]
        public void RateAnswer_Must_Return_NULL_If_RatedUp_Twice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingUp(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            var thisMustBeNull = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            //Assert
            Assert.True(thisMustBeNull == null);
        }
        [Fact]
        public void RateAnswer_Must_Return_Updated_Answer_RatedDown()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingDown(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);

            //Assert
            Assert.True(ratedAnswer != null);
            Assert.True(ratedAnswer.Rating == -1);
            Assert.True(ratedAnswer.QuestionId == addedQuestion.Id);
            Assert.True(ratedAnswer.PublisherId == testUser.Id);
            Assert.True(ratedAnswer.Content == answer.Content);
        }
        [Fact]
        public void RateAnswer_Must_Return_NULL_If_RatedDown_Twice()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingDown(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            var thisMustBeNull = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            //Assert
            Assert.True(thisMustBeNull == null);
        }
        [Fact]
        public void AddAnswer_Must_Return_Created_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);

            //Assert
            Assert.True(answer != null);
            Assert.True(answer.Content == content);
            Assert.True(answer.PublisherId == testUser.Id);
            Assert.True(answer.QuestionId == addedQuestion.Id);
        }
        [Fact]
        public void AddAnswer_Must_Return_NULL_If_There_Isnt_Question_With_Given_Id()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var content = "Test Answer";
            var testQuestionID = 10;
            var answer = discussionsService.AddAnswer(content, testUser, testQuestionID);

            //Assert
            Assert.True(answer == null);
        }
        [Fact]
        public void AddAnswer_Must_Return_NULL_If_Publisher_Isnt_Registered()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();

            //Act
            var content = "Test Answer";
            var nonUser = StaticMethods.GetTestUser();
            var questionId = 10;
            var answer = discussionsService.AddAnswer(content, nonUser, questionId);

            //Assert
            Assert.True(answer == null);
        }
        [Fact]
        public void DeleteAnswer_Must_Return_Deleted_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var deletedAnswer = discussionsService.DeleteAnswer(answer.Id, addedQuestion.Id,testUser.Id);

            //Assert
            Assert.True(deletedAnswer.Id == answer.Id);
        }
        [Fact]
        public void DeleteAnswer_Must_Return_NULL_If_There_Is_No_Answer_Or_Question_With_Given_Ids()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var testAnswerId = 23;
            var testQuestionId = 23;
            var deletedAnswer = discussionsService.DeleteAnswer(testAnswerId, testQuestionId,testUser.Id);

            //Assert
            Assert.True(deletedAnswer == null);
        }

        /// <summary>
        /// Like / Dislike addition function Tests
        /// </summary>
        [Fact]
        public void GetUserLikedAnswersIdList_Must_Return_User_Liked_Answers_Ids_In_List()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            var content1 = "Test Answer";
            var content2 = "Test Answer";

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();

            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);

            var answer1 = discussionsService.AddAnswer(content1, testUser, addedQuestion.Id);
            var answer2 = discussionsService.AddAnswer(content2, testUser, addedQuestion.Id);

            //// Like First Answer
            var testAnswerRatingBM1 = GetTestAnswerRatingBMRatingUp(answer1, testUser);
            var ratedAnswer1 = discussionsService.RateAnswer(testAnswerRatingBM1, testUser);

            //// Like Secont Answer
            testAnswerRatingBM1 = GetTestAnswerRatingBMRatingUp(answer2, testUser);
            var ratedAnswer2 = discussionsService.RateAnswer(testAnswerRatingBM1, testUser);

            var userLikedAnswers = discussionsService.GetUserLikedAnswersIdList(testUser.Id);
            //Assert

            Assert.True(userLikedAnswers.Count == 2);
            Assert.True(userLikedAnswers.Contains(answer1.Id));
            Assert.True(userLikedAnswers.Contains(answer2.Id));
        }
        [Fact]
        public void GetUserLikedAnswersIdList_Must_Return_Empty_List_If_User_Havent_Liked_Any_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var userLikedAnswers = discussionsService.GetUserLikedAnswersIdList(testUser.UserName);

            //Assert
            Assert.True(userLikedAnswers.Count == 0);
        }
        [Fact]
        public void GetUserDisLikedAnswersIdList_Must_Return_User_Liked_Answers_Ids_In_List()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            var content1 = "Test Answer";
            var content2 = "Test Answer";

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();

            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);

            var answer1 = discussionsService.AddAnswer(content1, testUser, addedQuestion.Id);
            var answer2 = discussionsService.AddAnswer(content2, testUser, addedQuestion.Id);

            //// Like First Answer
            var testAnswerRatingBM1 = GetTestAnswerRatingBMRatingDown(answer1, testUser);
            var ratedAnswer1 = discussionsService.RateAnswer(testAnswerRatingBM1, testUser);

            //// Like Secont Answer
            testAnswerRatingBM1 = GetTestAnswerRatingBMRatingDown(answer2, testUser);
            var ratedAnswer2 = discussionsService.RateAnswer(testAnswerRatingBM1, testUser);

            var userLikedAnswers = discussionsService.GetUserDisLikedAnswersIdList(testUser.Id);
            //Assert

            Assert.True(userLikedAnswers.Count == 2);
            Assert.True(userLikedAnswers.Contains(answer1.Id));
            Assert.True(userLikedAnswers.Contains(answer2.Id));
        }
        [Fact]
        public void GetUserDisLikedAnswersIdList_Must_Return_Empty_List_If_User_Havent_Disliked_Any_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var userLikedAnswers = discussionsService.GetUserDisLikedAnswersIdList(testUser.UserName);

            //Assert
            Assert.True(userLikedAnswers.Count == 0);
        }

        [Fact]
        public void IsUserLikedAnswer_Must_Return_True_If_User_Liked_Given_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingUp(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            var result = discussionsService.IsUserLikedAnswer(ratedAnswer.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserLikedAnswer_Must_Return_False_If_User_Not_Liked_Given_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var content = "Test Answer";
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var result = discussionsService.IsUserLikedAnswer(answer.Id, testUser.UserName);
            //Assert
            Assert.True(!result);
        }
        [Fact]
        public void IsUserDisLikedAnswer_Must_Return_True_If_User_DisLiked_Given_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            var content = "Test Answer";

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var testAnswerRatingBM = GetTestAnswerRatingBMRatingDown(answer, testUser);
            var ratedAnswer = discussionsService.RateAnswer(testAnswerRatingBM, testUser);
            var result = discussionsService.IsUserDisLikedAnswer(ratedAnswer.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserDisLikedAnswer_Must_Return_False_If_User_Not_DisLiked_Given_Answer()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            var content = "Test Answer";

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var answer = discussionsService.AddAnswer(content, testUser, addedQuestion.Id);
            var result = discussionsService.IsUserDisLikedAnswer(answer.Id, testUser.Id);
            //Assert
            Assert.True(!result);
        }

        [Fact]
        public void IsUserLikedQuestion_Must_Return_True_If_User_Liked_Given_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingUp(addedQuestion);
            var ratedQuestion = discussionsService.RateQuestion(questionRatingBindingModel, testUser);
            var result = discussionsService.IsUserLikedQuestion(ratedQuestion.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserLikedQuestion_Must_Return_False_If_User_Not_Liked_Given_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var result = discussionsService.IsUserLikedQuestion(addedQuestion.Id, testUser.UserName);
            //Assert
            Assert.True(!result);
        }
        [Fact]
        public void IsUserDisLikedQuestion_Must_Return_True_If_User_DisLiked_Given_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var questionRatingBindingModel = GetTestQuestionRatingBMRatingDown(addedQuestion);
            var ratedQuestion = discussionsService.RateQuestion(questionRatingBindingModel, testUser);
            var result = discussionsService.IsUserDisLikedQuestion(ratedQuestion.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserDisLikedQuestion_Must_Return_False_If_User_Not_Liked_Given_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var result = discussionsService.IsUserDisLikedQuestion(addedQuestion.Id, testUser.UserName);
            //Assert
            Assert.True(!result);
        }

        /// <summary>
        /// Testing Get* methods
        /// </summary>
        [Fact]
        public void GetQuestionViewModel_Must_Return_ViewModel()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var targetQuestion = discussionsService.GetQuestionViewModel(addedQuestion.Id);

            //Assert
            Assert.True(targetQuestion.QuestionId == addedQuestion.Id);
            Assert.True(targetQuestion.Content == addedQuestion.Content);
            Assert.True(targetQuestion.Category == addedQuestion.Category);
            Assert.True(targetQuestion.Title == addedQuestion.Title);
        }

        [Fact]
        public void GetQuestionViewModel_Must_Return_NULL_If_There_Isnt_Question_With_Given_Id()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testUser);
            var result = discussionsService.GetQuestionViewModel(addedQuestion.Id + 1);

            //Assert
            Assert.True(result == null);
        }

        [Fact]
        public void GetTopQuestion_Must_Return_Question_With_Highest_Rated_Question_By_Category()
        {
            // Testing with 2 categories
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser1 = StaticMethods.GetTestUser();
            var testUser2 = StaticMethods.GetTestUser();
            var testQBM1 = GetTestQuestionBM(Category.AndroidDevelopment);
            var testQBM2 = GetTestQuestionBM(Category.AndroidDevelopment);
            var testQBM3 = GetTestQuestionBM(Category.C);
            var testQBM4 = GetTestQuestionBM(Category.C);
            //Act
            db.Users.Add(testUser1);
            db.SaveChanges();
            var addedQuestion1 = discussionsService.AddQuestion(testQBM1, testUser1);
            var addedQuestion2 = discussionsService.AddQuestion(testQBM2, testUser1);
            var addedQuestion3 = discussionsService.AddQuestion(testQBM3, testUser1);
            var addedQuestion4 = discussionsService.AddQuestion(testQBM4, testUser1);
            discussionsService.RateQuestion(GetTestQuestionRatingBMRatingUp(addedQuestion1), testUser2);
            discussionsService.RateQuestion(GetTestQuestionRatingBMRatingDown(addedQuestion2), testUser2);

            discussionsService.RateQuestion(GetTestQuestionRatingBMRatingUp(addedQuestion3), testUser2);
            discussionsService.RateQuestion(GetTestQuestionRatingBMRatingDown(addedQuestion4), testUser2);

            var result = discussionsService.GetTopQuestions();
            //Assert
            Assert.True(result.Count == 2);
            Assert.True(result[0].QuestionId == addedQuestion1.Id);
            Assert.True(result[1].QuestionId == addedQuestion3.Id);
        }

        [Fact]
        public void DeleteQuestion_Must_Return_Deleted_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM(Category.AndroidDevelopment);
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM,testUser);
            var result = discussionsService.DeleteQuestion(addedQuestion.Id, testUser.Id);
            //Assert
            Assert.True(result.Id == addedQuestion.Id);
            Assert.True(result.Title == addedQuestion.Title);
            Assert.True(result.Rating == addedQuestion.Rating);
            Assert.True(result.PublishTime == addedQuestion.PublishTime);
            Assert.True(result.PublisherId == testUser.Id);
        }

        [Fact]
        public void EditQuestion_Must_Return_QuestionEditBM_Of_Given_Question()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM(Category.AndroidDevelopment);
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM,testUser);
            var editBM = discussionsService.GetQuestionEditBindingModel(addedQuestion.Id,testUser.Id);
            editBM.Title = "Testing buddy";
            editBM.Content = "This must be long string i think";
            editBM.Category = Category.JavaScript;
            editBM.Tags =  editBM.Tags+ " nice go back";
            var result = discussionsService.EditQuestion(editBM, testUser.Id);

            //Assert
            Assert.True(result.Id == addedQuestion.Id);
            Assert.True(result.Title == editBM.Title);
            Assert.True(result.Category == editBM.Category);

            Assert.True(result.Tags.Count == 4);
            Assert.True(result.Tags.Any(x => x.Name == "web"));
            Assert.True(result.Tags.Any(x => x.Name == "nice"));
            Assert.True(result.Tags.Any(x => x.Name == "go"));
            Assert.True(result.Tags.Any(x => x.Name == "back"));
        }

        [Fact]
        public void UpdateTags_Must_Return_List_Of_Updated_Tags()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM(Category.AndroidDevelopment);
            var testTags = "web nice os www nice";
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM,testUser);
            var result = discussionsService.UpdateTags(testTags, addedQuestion);
            //Assert
            Assert.True(result.Count == 4);
            Assert.True(result.Any(x => x.Name == "web"));
            Assert.True(result.Any(x=>x.Name == "www"));
            Assert.True(result.Any(x=>x.Name == "os"));
            Assert.True(result.Any(x=>x.Name == "nice"));
        }

        [Fact]
        public void UpdateTags_Must_Return_Empty_List_Of_Tags_If_There_Isnt_Any_Tag_To_Update()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM(Category.AndroidDevelopment);
            var testTags = "";
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM,testUser);
            var result = discussionsService.UpdateTags(testTags, addedQuestion);
            //Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetQuestionViewModels_Must_Return_Collection_Of_QuestionViewModels()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM1 = GetTestQuestionBM(Category.C);
            var testQBM2 = GetTestQuestionBM(Category.DesktopDevelopment);

            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion1 = discussionsService.AddQuestion(testQBM1, testUser);
            var addedQuestion2 = discussionsService.AddQuestion(testQBM2, testUser);

            var result = discussionsService.GetQuestionViewModels();

            //Assert
            Assert.True(result.Count == 2);
            Assert.True(result.Any(x=>x.Category == addedQuestion1.Category));
            Assert.True(result.Any(x=>x.Category == addedQuestion2.Category));
        }

        [Fact]
        public void GetAnswersViewModels_Must_Return_Collection_Of_Answers_From_Given_Question_Id()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testPublisher = StaticMethods.GetTestUser();
            var testUser = StaticMethods.GetTestUser("ChuckNorris","mail@chuck.com");
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.Users.Add(testPublisher);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testPublisher);
            var content1 = "First Test Answer";
            var answer1 = discussionsService.AddAnswer(content1, testUser, addedQuestion.Id);
            var content2 = "Second Test Answer";
            var answer2 = discussionsService.AddAnswer(content2, testUser, addedQuestion.Id);
            var result = discussionsService.GetAnswersViewModels(addedQuestion.Id);
            //Assert
            Assert.True(result.Count == 2);
            Assert.True(result.Any(x => x.AnswerId == answer1.Id));
            Assert.True(result.Any(x => x.AnswerId == answer2.Id));
        }

        [Fact]
        public void GetAnswersViewModels_Must_Return_Empty_Collection_If_There_Isnt_Question_With_Given_Id()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testPublisher = StaticMethods.GetTestUser();

            //Act
            db.Users.Add(testPublisher);
            db.SaveChanges();
            var result = discussionsService.GetAnswersViewModels(2312);
            //Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetUserAnswersVM_Must_Return_Collection_Of_Answers_From_Given_User_Name()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testPublisher = StaticMethods.GetTestUser();
            var testUser = StaticMethods.GetTestUser("ChuckNorris","mail@chuck.com");
            var testQBM = GetTestQuestionBM();

            //Act
            db.Users.Add(testUser);
            db.Users.Add(testPublisher);
            db.SaveChanges();
            var addedQuestion = discussionsService.AddQuestion(testQBM, testPublisher);
            var content1 = "First Test Answer";
            var answer1 = discussionsService.AddAnswer(content1, testUser, addedQuestion.Id);
            var content2 = "Second Test Answer";
            var answer2 = discussionsService.AddAnswer(content2, testUser, addedQuestion.Id);
            var result = discussionsService.GetUserAnswersVM(testUser.Id);
            //Assert
            Assert.True(result.Count == 2);
            Assert.True(result.Any(x => x.Content == answer1.Content));
            Assert.True(result.Any(x => x.Content == answer2.Content));
            
            Assert.True(result.Any(x => x.PublishTime == answer1.PublishTime));
            Assert.True(result.Any(x => x.PublishTime == answer2.PublishTime));

            Assert.True(result.Any(x => x.QuestionId == addedQuestion.Id));
            Assert.True(result.Any(x => x.QuestionTitle == addedQuestion.Title));
        }

        [Fact]
        public void GetUserAnswersVM_Must_Return_Empty_Collection_If_There_Isnt_User_With_User_Name()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testQBM = GetTestQuestionBM();

            //Act
            var result = discussionsService.GetUserAnswersVM("ChuckNorris");
            //Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetUserQuestionsVM_Must_Return_Collection_Of_Answers_From_Given_User_Name()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM1 = GetTestQuestionBM(Category.AndroidDevelopment);
            var testQBM2 = GetTestQuestionBM(Category.C);
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion1 = discussionsService.AddQuestion(testQBM1, testUser);
            var addedQuestion2 = discussionsService.AddQuestion(testQBM2, testUser);
            var result = discussionsService.GetUserQuestionsVM(testUser.Id);
            //Assert
            Assert.True(result.Count == 2);
            Assert.True(result.Any(x=>x.Id == addedQuestion1.Id));
            Assert.True(result.Any(x=>x.Id == addedQuestion2.Id));

            Assert.True(result.Any(x=>x.Title == addedQuestion1.Title));
            Assert.True(result.Any(x=>x.Title == addedQuestion2.Title));

            Assert.True(result.Any(x=>x.Category == addedQuestion1.Category));
            Assert.True(result.Any(x=>x.Category == addedQuestion2.Category));
        }

        [Fact]
        public void GetUserQuestionsVM_Must_Return_Empty_Collection_If_There_Isnt_User_With_User_Name()
        {
            // Arrange
            var db = StaticMethods.GetDb();
            var discussionsService = new DiscussionsService(db, this.mapper);
            var testUser = StaticMethods.GetTestUser();
            var testQBM = GetTestQuestionBM(Category.AndroidDevelopment);
            //Act
            db.Users.Add(testUser);
            db.SaveChanges();
            var addedQuestion1 = discussionsService.AddQuestion(testQBM, testUser);
            var result = discussionsService.GetUserQuestionsVM("ChuckNorris");
            //Assert
            Assert.True(result.Count == 0);
        }

        public AnswerRatingBindingModel GetTestAnswerRatingBMRatingUp(Answer answer, CustomUser user)
        {
            var model = new AnswerRatingBindingModel()
            {
                AnswerId = answer.Id,
                Username = user.UserName,
                Rating = 1
            };
            return model;
        }
        public AnswerRatingBindingModel GetTestAnswerRatingBMRatingDown(Answer answer, CustomUser user)
        {
            var model = new AnswerRatingBindingModel()
            {
                AnswerId = answer.Id,
                Username = user.UserName,
                Rating = -1
            };
            return model;
        }

        public QuestionBindingModel GetTestQuestionBM()
        {
            var model = new QuestionBindingModel()
            {
                Title = "TestQuestion",
                Content = "Just a test content",
                Tags = "web nice",
                Category = Category.WebDevelopment
            };
            return model;
        }
        public QuestionBindingModel GetTestQuestionBM(Category category)
        {
            var model = new QuestionBindingModel()
            {
                Title = "TestQuestion",
                Content = "Just a test content",
                Tags = "web nice",
                Category = category
            };
            return model;
        }

        public QuestionRatingBindingModel GetTestQuestionRatingBMRatingUp(Question question)
        {
            var model = new QuestionRatingBindingModel()
            {
                Rating = 1,
                QuestionId = question.Id,
                Username = "TestUser"
            };
            return model;
        }
        public QuestionRatingBindingModel GetTestQuestionRatingBMRatingDown(Question question)
        {
            var model = new QuestionRatingBindingModel()
            {
                Rating = -1,
                QuestionId = question.Id,
                Username = "TestUser"
            };
            return model;
        }
    }
}