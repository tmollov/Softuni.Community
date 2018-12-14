using System.Linq;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;

namespace Softuni.Community.Services
{
    public class DataService : IDataService
    {
        private readonly SuCDbContext context;
        
        public DataService(SuCDbContext context)
        {
            this.context = context;
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
    }
}
