using System.Linq;
using AutoMapper;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;

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

            this.context.SaveChanges();

            // Return updated entity if process is succesfull
            return userInfo;
        }

        public UserInfo UpdateProfilePicture(CustomUser user, string picUri)
        {
            var userInfo = this.context.UserInfos.FirstOrDefault(x => x.Id == user.UserInfoId);
            userInfo.ProfilePictureUrl = picUri;
            this.context.SaveChanges();

            // Return update entity if process is succesfull
            return userInfo;
        }

        public ProfilesSettingsBindingModel GetProfileSettingsBindingModel(int id)
        {
            var model = this.context.UserInfos.FirstOrDefault(x => x.Id == id);
            var result = mapper.Map<ProfilesSettingsBindingModel>(model);
            return result;
        }
    }
}