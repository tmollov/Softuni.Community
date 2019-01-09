using System;
using AutoMapper;
using Softuni.Community.Data.Models;
using Softuni.Community.Web;
using Softuni.Community.Web.Models.BindingModels;
using Xunit;


namespace Softuni.Community.Services.Tests
{
    public class UserServiceTests
    {
        public IMapper mapper = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        }).CreateMapper();


        [Fact]
        public void IsFirstUser_Must_Return_True_If_There_Is_Only_One_User()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();

            //Act
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();

            //Assert
            Assert.True(userService.IsFirstUser());
        }
        [Fact]
        public void IsFirstUser_Must_Return_False_If_There_Is_Two_or_More_Users()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser1 = StaticMethods.GetTestUser();
            var testUser2 = StaticMethods.GetTestUser();
            //Act
            dbContext.Users.Add(testUser1);
            dbContext.Users.Add(testUser2);
            dbContext.SaveChanges();

            //Assert
            Assert.True(userService.IsFirstUser() == false);
        }

        [Fact]
        public void AddUserInfo_Must_Return_Updated_UserInfo_with_Id()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUserInfo = GetTestUserInfo();
            var testUserInfo1 = GetTestUserInfo();
            var testUserInfo2 = GetTestUserInfo();

            //Act
            var userInfoRes = userService.AddUserInfo(testUserInfo);
            var userInfoRes1 = userService.AddUserInfo(testUserInfo1);
            var userInfoRes2 = userService.AddUserInfo(testUserInfo2);

            //Assert
            Assert.True(userInfoRes != null);
            Assert.True(userInfoRes1 != null);
            Assert.True(userInfoRes2 != null);
        }

        [Fact]
        public void UpdateUserInfo_Must_Return_Updated_UserInfo()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();
            var testUserInfo = GetTestUserInfo();
            var testUserInfoUpdate = GetTestUserInfoBMUpdate();
            //Act
            dbContext.UserInfos.Add(testUserInfo);
            dbContext.SaveChanges();
            testUser.UserInfo = testUserInfo;
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = userService.UpdateUserInfo(testUser, testUserInfoUpdate);

            //Assert
            Assert.True(result.FirstName == testUserInfoUpdate.FirstName);
            Assert.True(result.LastName == testUserInfoUpdate.LastName);
            Assert.True(result.BirthDate == testUserInfoUpdate.BirthDate);
            Assert.True(result.AboutMe == testUserInfoUpdate.AboutMe);
            Assert.True(result.ProfilePictureUrl == testUserInfoUpdate.ProfilePictureUrl);
        }

        [Fact]
        public void UpdateUserInfo_Must_Return_Same_UserInfo_With_Given_Same_UserInfo()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();
            var testUserInfo = GetTestUserInfo();
            var testSameUserInfoBM = GetTestSameUserInfoBM();
            //Act
            dbContext.UserInfos.Add(testUserInfo);
            dbContext.SaveChanges();
            testUser.UserInfo = testUserInfo;
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = userService.UpdateUserInfo(testUser, testSameUserInfoBM);

            //Assert
            Assert.True(result.FirstName == testSameUserInfoBM.FirstName);
            Assert.True(result.LastName == testSameUserInfoBM.LastName);
            Assert.True(result.BirthDate == testSameUserInfoBM.BirthDate);
            Assert.True(result.AboutMe == testSameUserInfoBM.AboutMe);
            Assert.True(result.ProfilePictureUrl == testSameUserInfoBM.ProfilePictureUrl);
        }

        [Fact]
        public void UpdateProfilePicture_Must_Return_Updated_UserInfo()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();
            var testUserInfo = GetTestUserInfo();
            //Act
            dbContext.UserInfos.Add(testUserInfo);
            dbContext.SaveChanges();

            testUser.UserInfo = testUserInfo;
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = userService.UpdateProfilePicture(testUser, null);

            //Assert
            Assert.True(result.FirstName == testUserInfo.FirstName);
            Assert.True(result.LastName == testUserInfo.LastName);
            Assert.True(result.BirthDate == testUserInfo.BirthDate);
            Assert.True(result.AboutMe == testUserInfo.AboutMe);
            Assert.True(result.ProfilePictureUrl == null);
        }

        [Fact]
        public void GetProfileSettingsBindingModel_Must_Return_BindingModel()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();
            var testUserInfo = GetTestUserInfo();
            //Act
            dbContext.UserInfos.Add(testUserInfo);
            dbContext.SaveChanges();

            testUser.UserInfo = testUserInfo;
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = userService.GetProfileSettingsBindingModel(testUserInfo.Id);

            //Assert
            Assert.True(result.FirstName == testUserInfo.FirstName);
            Assert.True(result.LastName == testUserInfo.LastName);
            Assert.True(result.BirthDate == testUserInfo.BirthDate);
            Assert.True(result.AboutMe == testUserInfo.AboutMe);
            Assert.True(result.ProfilePictureUrl == testUserInfo.ProfilePictureUrl);
        }

        [Fact]
        public void GetProfileViewModel_Must_Return_ViewModel()
        {
            // Arrange
            var dbContext = StaticMethods.GetDb();
            var userService = new UserService(dbContext, mapper);
            var testUser = StaticMethods.GetTestUser();
            var testUserInfo = GetTestUserInfo();
            //Act
            dbContext.UserInfos.Add(testUserInfo);
            dbContext.SaveChanges();

            testUser.UserInfo = testUserInfo;
            dbContext.Users.Add(testUser);
            dbContext.SaveChanges();
            var result = userService.GetProfileViewModel(testUserInfo.Id);

            //Assert
            Assert.True(result.FirstName == testUserInfo.FirstName);
            Assert.True(result.LastName == testUserInfo.LastName);
            Assert.True(result.BirthDate == testUserInfo.BirthDate);
            Assert.True(result.AboutMe == testUserInfo.AboutMe);
            Assert.True(result.ProfilePictureUrl == testUserInfo.ProfilePictureUrl);
        }

        public UserInfo GetTestUserInfo()
        {
            var UserInfo = new UserInfo()
            {
                BirthDate = DateTime.Now,
                FirstName = "ImTestFirstName",
                LastName = "ImTestFirstName",
                AboutMe = "Someone who want to be e Developer!",
                //Just pic from google
                ProfilePictureUrl = "http://deafhhcenter.org/wp-content/uploads/2017/12/profile-default.jpg"
            };

            return UserInfo;
        }

        public UserInfoBindingModel GetTestSameUserInfoBM()
        {
            var userInfo = this.GetTestUserInfo();
            var bm = mapper.Map<UserInfoBindingModel>(userInfo);
            return bm;
        }
        public UserInfoBindingModel GetTestUserInfoBMUpdate()
        {
            var UserInfoBM = mapper.Map<UserInfoBindingModel>(

                new UserInfo()
                {
                    BirthDate = new DateTime(1997, 2, 2),
                    FirstName = "ImUpdateFirstName",
                    LastName = "ImUpdateFirstName",
                    AboutMe = "Just a Dev",
                    ProfilePictureUrl = "http://deafhhcenter.org/wp-content/uploads/2017/12/profile-default.jpg"
                });

            return UserInfoBM;
        }
    }
}