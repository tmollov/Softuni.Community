using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Data.Models.Enums;
using Softuni.Community.Web;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;
using Xunit;


namespace Softuni.Community.Services.Tests
{
    public class JokesServiceTests
    {
        public IMapper mapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        }).CreateMapper();

        [Fact]
        public void AddJoke_Must_Return_Created_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = jokesService.AddJoke(testJokeBM, testUser);

            //Assert
            Assert.True(result.Content == testJokeBM.Content);
            Assert.True(result.Category == testJokeBM.Category);
            Assert.True(result.Likes == 0);
            Assert.True(result.Dislikes == 0);
            Assert.True(result.PublisherId == testUser.Id);
        }
        [Fact]
        public void DeleteJoke_Must_Return_Deleted_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.DeleteJoke(addedJoke.Id);
            //Assert
            Assert.True(result.Id == addedJoke.Id);
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.PublisherId == addedJoke.PublisherId);
        }
        [Fact]
        public void EditJoke_Must_Return_Edited_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var editJokeBM = GetTestJokeEditBM(addedJoke.Id);
            var editedJoke = jokesService.EditJoke(editJokeBM);

            //Assert
            Assert.True(editedJoke.Content == addedJoke.Content);
            Assert.True(editedJoke.Category == addedJoke.Category);
            Assert.True(editedJoke.Likes == 0);
            Assert.True(editedJoke.Dislikes == 0);
            Assert.True(editedJoke.PublisherId == testUser.Id);
        }

        [Fact]
        public void RateJoke_Must_Return_Rated_Joke_If_RatedUp()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedUpBM(addedJoke.Id, testUser.UserName);
            var result = jokesService.RateJoke(testJokeRatingBM, testUser);
            //Assert
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.Likes == 1);
            Assert.True(result.Dislikes == 0);
            Assert.True(result.PublisherId == testUser.Id);
        }
        [Fact]
        public void RateJoke_Must_Return_NULL_If_Joke_RatedUp_Twice()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedUpBM(addedJoke.Id, testUser.UserName);
            var firstTime = jokesService.RateJoke(testJokeRatingBM, testUser);
            var secondTime = jokesService.RateJoke(testJokeRatingBM, testUser);
            //Assert
            Assert.True(secondTime == null);
        }
        [Fact]
        public void RateJoke_Must_Return_Rated_Joke_If_RatedDowns()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedDownBM(addedJoke.Id, testUser.UserName);
            var result = jokesService.RateJoke(testJokeRatingBM, testUser);
            //Assert
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.Likes == 0);
            Assert.True(result.Dislikes == -1);
            Assert.True(result.PublisherId == testUser.Id);
        }
        [Fact]
        public void RateJoke_Must_Return_NULL_If_Joke_RatedDown_Twice()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedDownBM(addedJoke.Id, testUser.UserName);
            var firstTime = jokesService.RateJoke(testJokeRatingBM, testUser);
            var secondTime = jokesService.RateJoke(testJokeRatingBM, testUser);
            //Assert
            Assert.True(secondTime == null);
        }

        [Fact]
        public void GetJoke_Must_Return_Joke_When_T_Is_Joke_Type()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetJoke<Joke>(addedJoke.Id);
            //Assert
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.Likes == 0);
            Assert.True(result.Dislikes == 0);
            Assert.True(result.PublisherId == testUser.Id);
        }
        [Fact]
        public void GetJoke_Must_Return_Joke_When_T_Is_JokeViewModel()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetJoke<JokeViewModel>(addedJoke.Id);
            //Assert
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.Likes == 0);
            Assert.True(result.Dislikes == 0);
            Assert.True(result.Publisher == testUser.UserName);
        }
        [Fact]
        public void GetJoke_Must_Return_Joke_When_T_Is_JokeEditBindingModel()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetJoke<JokeEditBindingModel>(addedJoke.Id);
            //Assert
            Assert.True(result.Content == addedJoke.Content);
            Assert.True(result.Category == addedJoke.Category);
            Assert.True(result.Id == addedJoke.Id);

        }

        [Fact]
        public void GetAllJokes_Must_Return_AllJokesViewModel()
        {
            // Arrange
            var dbContext = this.GetDb();
            dbContext.Database.EnsureDeleted();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            jokesService.AddJoke(testJokeBM, testUser);
            jokesService.AddJoke(testJokeBM, testUser);
            jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetAllJokes();
            //Assert
            Assert.True(result.Jokes.Count == 3);
        }

        [Fact]
        public void IsUserLikedJoke_Must_Return_TRUE_If_User_Likes_Given_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedUpBM(addedJoke.Id, testUser.UserName);
            var ratedJoke = jokesService.RateJoke(testJokeRatingBM, testUser);
            var result = jokesService.IsUserLikedJoke(addedJoke.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserLikedJoke_Must_Return_FALSE_If_User_Likes_Given_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.IsUserLikedJoke(addedJoke.Id, testUser.Id);
            //Assert
            Assert.True(!result);
        }

        [Fact]
        public void IsUserDislikedJoke_Must_Return_TRUE_If_User_Likes_Given_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var testJokeRatingBM = GetTestJokeRatedDownBM(addedJoke.Id, testUser.UserName);
            var ratedJoke = jokesService.RateJoke(testJokeRatingBM, testUser);
            var result = jokesService.IsUserDisLikedJoke(addedJoke.Id, testUser.Id);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsUserDislikedJoke_Must_Return_FALSE_If_User_Likes_Given_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.IsUserDisLikedJoke(addedJoke.Id, testUser.Id);
            //Assert
            Assert.True(!result);
        }

        [Fact]
        public void GetUserLikedJokesIdList_Must_Return_Id_List_Of_User_Liked_Jokes()
        {
            // Arrange
            var dbContext = this.GetDb();
            dbContext.Database.EnsureDeleted();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var Joke1 = jokesService.AddJoke(testJokeBM, testUser);
            var Joke2 = jokesService.AddJoke(testJokeBM, testUser);
            var Joke3 = jokesService.AddJoke(testJokeBM, testUser);
            var ratedJoke1 = GetTestJokeRatedUpBM(Joke1.Id, testUser.UserName);
            var ratedJoke2 = GetTestJokeRatedUpBM(Joke2.Id, testUser.UserName);
            var ratedJoke3 = GetTestJokeRatedUpBM(Joke3.Id, testUser.UserName);
            jokesService.RateJoke(ratedJoke1, testUser);
            jokesService.RateJoke(ratedJoke2, testUser);
            jokesService.RateJoke(ratedJoke3, testUser);

            var result = jokesService.GetUserLikedJokesIdList(testUser.Id);
            //Assert
            Assert.True(result.Contains(Joke1.Id));
            Assert.True(result.Contains(Joke2.Id));
            Assert.True(result.Contains(Joke3.Id));
        }
        [Fact]
        public void GetUserLikedJokesIdList_Must_Return_Empty_Collection_If_User_Havent_Liked_Any_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetUserLikedJokesIdList(testUser.Id);
            //Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetUserDislikedJokesIdList_Must_Return_Id_List_Of_User_Liked_Jokes()
        {
            // Arrange
            var dbContext = this.GetDb();
            dbContext.Database.EnsureDeleted();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var Joke1 = jokesService.AddJoke(testJokeBM, testUser);
            var Joke2 = jokesService.AddJoke(testJokeBM, testUser);
            var Joke3 = jokesService.AddJoke(testJokeBM, testUser);
            var ratedJoke1 = GetTestJokeRatedDownBM(Joke1.Id, testUser.UserName);
            var ratedJoke2 = GetTestJokeRatedDownBM(Joke2.Id, testUser.UserName);
            var ratedJoke3 = GetTestJokeRatedDownBM(Joke3.Id, testUser.UserName);
            jokesService.RateJoke(ratedJoke1, testUser);
            jokesService.RateJoke(ratedJoke2, testUser);
            jokesService.RateJoke(ratedJoke3, testUser);

            var result = jokesService.GetUserDislikedJokesIdList(testUser.Id);
            //Assert
            Assert.True(result.Contains(Joke1.Id));
            Assert.True(result.Contains(Joke2.Id));
            Assert.True(result.Contains(Joke3.Id));
        }
        [Fact]
        public void GetUserDislikedJokesIdList_Must_Return_Empty_Collection_If_User_Havent_Disliked_Any_Joke()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM = GetTestJokeBM();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var addedJoke = jokesService.AddJoke(testJokeBM, testUser);
            var result = jokesService.GetUserDislikedJokesIdList(testUser.Id);
            //Assert
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetTopJokes_Must_Return_Top_Jokes_From_Every_Category()
        {
            // Arrange
            var dbContext = this.GetDb();
            var jokesService = new JokesService(dbContext, mapper);
            var testUser = GetTestUser();
            var testJokeBM1 = GetTestJokeBM(JokeCategory.ChuckNorris);
            var testJokeBM2 = GetTestJokeBM(JokeCategory.Animals);
            var testJokeBM3 = GetTestJokeBM(JokeCategory.Computers);
            var testJokeBM4 = GetTestJokeBM(JokeCategory.DriversAndPilots);
            var testJokeBM5 = GetTestJokeBM(JokeCategory.Ivancho);
            var testJokeBM6 = GetTestJokeBM(JokeCategory.Students);
            var testJokeBM7 = GetTestJokeBM(JokeCategory.Different);
            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            jokesService.AddJoke(testJokeBM1, testUser);
            jokesService.AddJoke(testJokeBM2, testUser);
            jokesService.AddJoke(testJokeBM3, testUser);
            jokesService.AddJoke(testJokeBM4, testUser);
            jokesService.AddJoke(testJokeBM5, testUser);
            jokesService.AddJoke(testJokeBM6, testUser);
            jokesService.AddJoke(testJokeBM7, testUser);
            var topJokes = jokesService.GetTopJokes(testUser.Id);
            //Assert
            Assert.True(topJokes.Count == 7);
        }

        public JokeRatingBindingModel GetTestJokeRatedUpBM(int id, string username)
        {
            var bm = new JokeRatingBindingModel()
            {
                JokeId = id,
                Rating = 1,
                Username = username
            };
            return bm;
        }
        public JokeRatingBindingModel GetTestJokeRatedDownBM(int id, string username)
        {
            var bm = new JokeRatingBindingModel()
            {
                JokeId = id,
                Rating = -1,
                Username = username
            };
            return bm;
        }
        public JokeEditBindingModel GetTestJokeEditBM(int id)
        {
            var bm = new JokeEditBindingModel()
            {
                Id = id,
                Content = "This is edited joke content for testing!!! and yea still valid content",
                Category = JokeCategory.Students
            };
            return bm;
        }
        public JokeBindingModel GetTestJokeBM()
        {
            var jokeBM = new JokeBindingModel()
            {
                Content = "This is a valid joke with minimum 10 characters... ups *20 characters",
                Category = JokeCategory.Different
            };
            return jokeBM;
        }
        public JokeBindingModel GetTestJokeBM(JokeCategory category)
        {
            var jokeBM = new JokeBindingModel()
            {
                Content = "This is a valid joke with minimum 10 characters... ups *20 characters",
                Category = category
            };
            return jokeBM;
        }
        public CustomUser GetTestUser()
        {
            var testUser = new CustomUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mail@mail.com",
                UserName = "TestUser",
                PasswordHash = "MySecretPass1",
            };
            return testUser;
        }
        public SuCDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SuCDbContext>()
                .UseInMemoryDatabase(databaseName: "SuC-InMemory")
                .Options;
            var dbContext = new SuCDbContext(dbOptions);
            return dbContext;
        }
    }
}
