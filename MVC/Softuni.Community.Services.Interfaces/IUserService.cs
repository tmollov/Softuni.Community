using Softuni.Community.Data.Models;

namespace Softuni.Community.Services.Interfaces
{
    public interface IUserService
    {
        bool IsFirstUser();
        UserInfo AddUserInfo(UserInfo userInfo);
        UserInfo UpdateUserInfo(CustomUser user, UserInfo userInfo);
    }
}
