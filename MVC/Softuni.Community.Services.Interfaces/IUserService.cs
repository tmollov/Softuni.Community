using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IUserService
    {
        bool IsFirstUser();
        UserInfo AddUserInfo(UserInfo userInfo);
        UserInfo UpdateUserInfo(CustomUser user, UserInfoBindingModel userInfo);
        ProfilesSettingsBindingModel GetProfileSettingsBindingModel(int id);
        UserInfo UpdateProfilePicture(CustomUser user, string picUri);
        MyProfileViewModel GetProfileViewModel(int id);
    }
}
