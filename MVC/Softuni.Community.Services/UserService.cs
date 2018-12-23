using System.Linq;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;

namespace Softuni.Community.Services
{
    public class UserService : IUserService
    {
        private readonly SuCDbContext context;
        public UserService(SuCDbContext context)
        {
            this.context = context;
        }

        //Tested
        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            this.context.UserInfos.Add(userInfo);
            this.context.SaveChanges();
            return userInfo;
        }

        //Tested
        public bool IsFirstUser()
        {
            return context.Users.Count() == 1;
        }

        public UserInfo UpdateUserInfo(CustomUser user, UserInfo newUserInfo)
        {
            var userInfo = this.context.UserInfos.FirstOrDefault(x => x.Id == user.UserInfoId);
            userInfo.FirstName = newUserInfo.FirstName;
            userInfo.LastName = newUserInfo.LastName;
            userInfo.BirthDate = newUserInfo.BirthDate;
            userInfo.AboutMe = newUserInfo.AboutMe;
            userInfo.ProfilePictureUrl = newUserInfo.ProfilePictureUrl;
            this.context.SaveChanges();

            // Return update entity if process is succesfull
            return userInfo;
        }
    }
}