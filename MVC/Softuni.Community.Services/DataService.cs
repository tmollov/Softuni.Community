using System.Linq;
using Microsoft.AspNetCore.Identity;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;

namespace Softuni.Community.Services
{
    public class DataService : IDataService
    {
        private readonly SuCDbContext context;
        private readonly UserManager<CustomUser> userMgr;

        public DataService(SuCDbContext context,UserManager<CustomUser> userMgr)
        {
            this.context = context;
            this.userMgr = userMgr;
        }

        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            this.context.UserInfos.Add(userInfo);
            this.context.SaveChanges();
            return userInfo;
        }

        public bool IsFirstUser()
        {
            return context.Users.Count() == 1;
        }

        public UserInfo UpdateUserInfo(string username, UserInfo newUserInfo)
        {
            CustomUser user = userMgr.FindByNameAsync(username).Result;
            if (user != null)
            {
                var userInfo = this.context.UserInfos.FirstOrDefault(x => x.Id == user.UserInfoId);
                userInfo.FirstName = newUserInfo.FirstName;
                userInfo.LastName = newUserInfo.LastName;
                userInfo.BirthDate = newUserInfo.BirthDate;
                userInfo.AboutMe = newUserInfo.AboutMe;
                this.context.SaveChanges();
                
                // Return same entity if process is succesfull
                return newUserInfo;
            }

            // Return null if process is failed
            return null;
        }
    }
}
