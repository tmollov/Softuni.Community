using System.Linq;
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
            this.CreateMap<ProfilesSettingsBindingModel, UserInfoBindingModel>();
            this.CreateMap<UserInfoBindingModel, UserInfo>();
            this.CreateMap<RegisterBindingModel, CustomUser>();
            this.CreateMap<UserInfo, MyProfileViewModel>();
            this.CreateMap<UserInfo, ProfilesSettingsBindingModel>();

            this.CreateMap<JokeBindingModel, Joke>();
            this.CreateMap<Joke, JokeEditBindingModel>();
            this.CreateMap<Joke, JokeViewModel>()
                .ForMember(x => x.Publisher,
                    opts => opts.MapFrom(src => src.Publisher.UserName));

            this.CreateMap<Question, QuestionEditBindingModel>()
                .ForMember(x => x.Tags,
                    opts => opts.MapFrom(src => string.Join(" ", src.Tags.Select(a => a.Name))));

            this.CreateMap<Question, QuestionViewModel>()
                .ForMember(x => x.QuestionId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.AnswerCount,
                    opts => opts.MapFrom(src => src.Answers.Count))
                .ForMember(x => x.PublisherName,
                    opts => opts.MapFrom(src => src.Publisher.UserName))
                .ForMember(x => x.PublisherPicture,
                    opts => opts.MapFrom(src => src.Publisher.UserInfo.ProfilePictureUrl))
                .ForMember(x => x.Tags,
                    opts => opts.MapFrom(src => src.Tags.Select(a => a.Name).ToList()));

            this.CreateMap<Question, QuestionViewModel>()
                .ForMember(x => x.QuestionId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.AnswerCount,
                    opts => opts.MapFrom(src => src.Answers.Count))
                .ForMember(x => x.PublisherName,
                    opts => opts.MapFrom(src => src.Publisher.UserName))
                .ForMember(x => x.PublisherPicture,
                         opts => opts.MapFrom(src => src.Publisher.UserInfo.ProfilePictureUrl))
                .ForMember(x => x.Tags,
                    opts => opts.MapFrom(src => src.Tags.Select(a => a.Name).ToList()));

            this.CreateMap<Question, MyQuestionViewModel>()
                .ForMember(x => x.AnswerCount,
                    opts => opts.MapFrom(src => src.Answers.Count));

            this.CreateMap<Answer, AnswerViewModel>()
                .ForMember(x => x.AnswerId,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.PublisherName,
                    opts => opts.MapFrom(src => src.Publisher.UserName))
                .ForMember(x => x.PublisherPicture,
                    opts => opts.MapFrom(src => src.Publisher.UserInfo.ProfilePictureUrl));

            this.CreateMap<Answer, MyAnswerViewModel>()
                .ForMember(x => x.QuestionTitle,
                    opts => opts.MapFrom(src => src.Question.Title));

            this.CreateMap<AddProblemBindingModel, GameProblem>()
                .ForMember(x => x.ProblemContent,
                    opts => opts.MapFrom(src => src.Content));

            this.CreateMap<GameProblem, GameProblemViewModel>();
            this.CreateMap<GameProblem, ProblemDetailsViewModel>()
                .ForMember(x => x.Answers,
                    opts => opts.MapFrom(src => src.Choices.Select(x => x.Content).ToArray()));

            this.CreateMap<GameProblem, EditProblemBindingModel>()
                .ForMember(x => x.RightAnswer, 
                    opt => opt.Ignore())
                .ForMember(x => x.Content,
                    opts => opts.MapFrom(src => src.ProblemContent))
                .ForMember(x => x.AnswerA,
                    opts => opts.MapFrom(src => src.Choices[0].Content))
                .ForMember(x => x.AnswerB,
                    opts => opts.MapFrom(src => src.Choices[1].Content))
                .ForMember(x => x.AnswerC,
                    opts => opts.MapFrom(src => src.Choices[2].Content))
                .ForMember(x => x.AnswerD,
                    opts => opts.MapFrom(src => src.Choices[3].Content));


        }
    }
}