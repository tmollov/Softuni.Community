using AutoMapper;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;

namespace Softuni.Community.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<UserInfoBindingModel, UserInfo>();
            this.CreateMap<RegisterBindingModel, CustomUser>();
        }
    }
}