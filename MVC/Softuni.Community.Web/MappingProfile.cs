﻿using System.Linq;
using AutoMapper;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<UserInfoBindingModel, UserInfo>();
            this.CreateMap<RegisterBindingModel, CustomUser>();

            this.CreateMap<Question, QuestionViewModel>()
                .ForMember(x=>x.QuestionId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(x=>x.AnswerCount,
                    opts => opts.MapFrom(src => src.Answers.Count))
                .ForMember(x=>x.PublisherName,
                    opts => opts.MapFrom(src => src.Publisher.UserName))
                .ForMember(x=>x.PublisherPicture,
                         opts => opts.MapFrom(src => src.Publisher.UserInfo.ProfilePictureUrl))
                .ForMember(x=>x.Tags,
                    opts => opts.MapFrom(src => src.Tags.Select(a => a.Name).ToList()));

            this.CreateMap<Answer, AnswerViewModel>()
                .ForMember(x => x.AnswerId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.PublisherName,
                    opts => opts.MapFrom(src => src.Publisher.UserName))
                .ForMember(x => x.PublisherPicture,
                    opts => opts.MapFrom(src => src.Publisher.UserInfo.ProfilePictureUrl));
        }
    }
}