using System.Collections.Generic;
using Softuni.Community.Data.Models;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services.Interfaces
{
    public interface IJokesService
    {
        Joke AddJoke(JokeBindingModel model, CustomUser publisher);
        Joke DeleteJoke(int id);
        Joke EditJoke(JokeEditBindingModel model);
        T GetJoke<T>(int id);
        Joke RateJoke(JokeRatingBindingModel ratingModel, CustomUser user);
        AllJokesViewModel GetAllJokes();

        IList<int> GetUserLikedJokesIdList(string userId);
        IList<int> GetUserDislikedJokesIdList(string userId);

        bool IsUserDisLikedJoke(int jokeId, string userId);
        bool IsUserLikedJoke(int jokeId, string userId);
    }
}
