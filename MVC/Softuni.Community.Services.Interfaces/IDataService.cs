using Softuni.Community.Data.Models;

namespace Softuni.Community.Services.Interfaces
{
    public interface IDataService
    {
        bool IsFirstUser();
        UserInfo AddUserInfo(UserInfo userInfo);
    }
}
