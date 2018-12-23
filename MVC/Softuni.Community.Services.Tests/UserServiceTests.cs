using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Xunit;


namespace Softuni.Community.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void IsFirstUser_Must_Return_True_If_There_Is_Only_One_User()
        {
            // Arrange
            var dbContext = this.GetDb();
            //// Clear Users
            dbContext.Database.EnsureDeleted();
            var userService = new UserService(dbContext);
            var testUser = GetTestUser();

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
            var dbContext = this.GetDb();
            //// Clear Users
            dbContext.Database.EnsureDeleted();
            var userService = new UserService(dbContext);
            var testUser1 = GetTestUser();
            var testUser2 = GetTestUser();
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
            var dbContext = this.GetDb();
            var userService = new UserService(dbContext);
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
        public void UpdateUserInfo_Must_Return_Updated_UserInfo_If_Successeed()
        {
            // Arrange
            var dbContext = this.GetDb();
            var userService = new UserService(dbContext);
            var testUser = GetTestUser();
            var testUserInfo = GetTestUserInfo();
            var testUserInfoUpdate = GetTestUserInfoUpdate();
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

        public CustomUser GetTestUser()
        {
            var testUser = new CustomUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mail@mail.com",
                UserName = "TestUser",
                PasswordHash = "MySecretPass1"
            };
            return testUser;
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
        public UserInfo GetTestUserInfoUpdate()
        {
            var UserInfo = new UserInfo()
            {
                BirthDate = new DateTime(1997, 2, 2),
                FirstName = "ImUpdateFirstName",
                LastName = "ImUpdateFirstName",
                AboutMe = "Just a Dev",
                ProfilePictureUrl = null
            };

            return UserInfo;
        }

        public SuCDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SuCDbContext>()
                .UseInMemoryDatabase(databaseName: "SuC-InMemory")
                .Options;
            var dbContext = new SuCDbContext(dbOptions);
            return dbContext;
        }

        public static Mock<UserManager<CustomUser>> MockUserManager<CustomUser>(List<CustomUser> ls) where CustomUser : class
        {
            var store = new Mock<IUserStore<CustomUser>>();
            var mgr = new Mock<UserManager<CustomUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<CustomUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<CustomUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<CustomUser>()))
                .ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<CustomUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success).Callback<CustomUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<CustomUser>()))
                .ReturnsAsync(IdentityResult.Success);
            //mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            //    .ReturnsAsync(CustomUser);

            return mgr;
        }
        public UserManager<CustomUser> GetUserManager()
        {
            var mockUserStore = new Mock<IUserStore<CustomUser>>();
            var mockIOption = new Mock<IOptions<IdentityOptions>>();
            var mockIPasswordHasher = new Mock<PasswordHasher<CustomUser>>();
            var mockIUserValidator = new Mock<IEnumerable<IUserValidator<CustomUser>>>();
            var mockIPasswordValidator = new Mock<IEnumerable<IPasswordValidator<CustomUser>>>();
            var mockILookupNormalizer = new Mock<ILookupNormalizer>();
            var mockIdentityErrorDecriber = new Mock<IdentityErrorDescriber>();
            var mockIServiceProvider = new Mock<IServiceProvider>();
            var mockILogger = new Mock<ILogger<UserManager<CustomUser>>>();

            var userManager = new UserManager<CustomUser>(mockUserStore.Object,
                mockIOption.Object,
                mockIPasswordHasher.Object,
                mockIUserValidator.Object,
                mockIPasswordValidator.Object,
                mockILookupNormalizer.Object,
                mockIdentityErrorDecriber.Object,
                mockIServiceProvider.Object,
                mockILogger.Object);
            return userManager;
        }
        public class FakeUserManager : UserManager<CustomUser>
        {

            public FakeUserManager()
                : base(new Mock<IUserStore<CustomUser>>().Object, null, null, null, null, null, null, null, null)
            { }

            public override Task<IdentityResult> CreateAsync(CustomUser user, string password)
            {
                var cansellationToken = new CancellationToken();
                var res = this.Store.CreateAsync(user, cansellationToken);
                return res;
            }

            public override Task<CustomUser> FindByNameAsync(string username)
            {
                var cansellationToken = new CancellationToken();
                return this.Store.FindByNameAsync(username, cansellationToken);
            }

            public override Task<IdentityResult> AddToRoleAsync(CustomUser user, string role)
            {
                return Task.FromResult(IdentityResult.Success);
            }

            public override Task<string> GenerateEmailConfirmationTokenAsync(CustomUser user)
            {
                return Task.FromResult(Guid.NewGuid().ToString());
            }

        }
    }
}
