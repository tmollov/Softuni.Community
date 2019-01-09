using System.Linq;
using AutoMapper;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services
{
    public class UserService : IUserService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;

        public UserService(SuCDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        //Tested
        public UserInfo UpdateUserInfo(CustomUser user, UserInfoBindingModel newUserInfo)
        {
            var userInfo = this.context.UserInfos.FirstOrDefault(x => x.Id == user.UserInfoId);
            if (newUserInfo.FirstName != null)
                userInfo.FirstName = newUserInfo.FirstName;
            if (newUserInfo.LastName != null)
                userInfo.LastName = newUserInfo.LastName;
            if (newUserInfo.BirthDate != null)
                userInfo.BirthDate = newUserInfo.BirthDate;
            if (newUserInfo.AboutMe != null)
                userInfo.AboutMe = newUserInfo.AboutMe;
            if (newUserInfo.ProfilePictureUrl != null)
                userInfo.ProfilePictureUrl = newUserInfo.ProfilePictureUrl;
            if (newUserInfo.State != null)
                userInfo.State = newUserInfo.State;
            this.context.SaveChanges();
            
            return userInfo;
        }

        //Tested
        public UserInfo UpdateProfilePicture(CustomUser user, string picUri)
        {
            var userInfo = this.context.UserInfos.FirstOrDefault(x => x.Id == user.UserInfoId);
            userInfo.ProfilePictureUrl = picUri;
            this.context.SaveChanges();
            
            return userInfo;
        }

        //Tested
        public ProfilesSettingsBindingModel GetProfileSettingsBindingModel(int id)
        {
            var model = this.context.UserInfos.FirstOrDefault(x => x.Id == id);
            var result = mapper.Map<ProfilesSettingsBindingModel>(model);
            return result;
        }

        //Tested
        public MyProfileViewModel GetProfileViewModel(int id)
        {
            var model = this.context.UserInfos.FirstOrDefault(x => x.Id == id);
            var result = mapper.Map<MyProfileViewModel>(model);
            return result;
        }

        public bool FinishSetUp(string userId)
        {
            // Returns false if ser profile is already set up.

            var user = this.context.Users.FirstOrDefault(x => x.Id == userId);
            if (user.IsProfileSetUp != true)
            {
                user.IsProfileSetUp = true;
                this.context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}