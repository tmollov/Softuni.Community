using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data;
using Softuni.Community.Data.Models;
using Softuni.Community.Services.Interfaces;
using Softuni.Community.Web.Models.BindingModels;
using Softuni.Community.Web.Models.ViewModels;

namespace Softuni.Community.Services
{
    public class JokesService : IJokesService
    {
        private readonly SuCDbContext context;
        private readonly IMapper mapper;

        public JokesService(SuCDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Joke AddJoke(JokeBindingModel model, CustomUser publisher)
        {
            var joke = this.mapper.Map<Joke>(model);
            joke.Content = joke.Content.Trim();
            joke.Publisher = publisher;
            this.context.Jokes.Add(joke);
            this.context.SaveChanges();
            return joke;
        }

        public AllJokesViewModel GetAllJokes()
        {
            var jokes = this.context.Jokes
                .Include(x => x.Publisher)
                .Select(x => mapper.Map<JokeViewModel>(x))
                .ToList();
            ;

            var vm = new AllJokesViewModel()
            {
                Jokes = jokes
            };
            return vm;
        }

        public Joke DeleteJoke(int id)
        {
            var joke = this.context.Jokes.FirstOrDefault(x => x.Id == id);
            this.context.Jokes.Remove(joke);
            this.context.SaveChanges();
            return joke;
        }

        public T GetJoke<T>(int id)
        {
            var joke = this.context.Jokes.Include(x => x.Publisher).FirstOrDefault(x => x.Id == id);
            var result = mapper.Map<T>(joke);
            return result;
        }

        public Joke EditJoke(JokeEditBindingModel model)
        {
            var jokeToEdit = this.context.Jokes.FirstOrDefault(x => x.Id == model.Id);
            jokeToEdit.Content = model.Content;
            jokeToEdit.Category = model.Category;
            this.context.SaveChanges();
            return jokeToEdit;
        }

        public Joke RateJoke(JokeRatingBindingModel ratingModel, CustomUser user)
        {
            var joke = this.context.Jokes.FirstOrDefault(x => x.Id == ratingModel.JokeId);

            if (ratingModel.Rating == 1)
            {
                joke.Likes++;
                var jokeToRate = new UserJokeLikes(ratingModel.JokeId, user);
                this.context.UsersJokeLikes.Add(jokeToRate);

                if (IsUserDisLikedJoke(ratingModel.JokeId, user.Id))
                {
                    var dislikedJoke = this.context.UsersJokeDislikes.FirstOrDefault(x =>
                        x.User.UserName == ratingModel.Username && x.JokeId == ratingModel.JokeId);
                    this.context.UsersJokeDislikes.Remove(dislikedJoke);
                    this.context.SaveChanges();
                }
            }
            else
            {
                joke.Dislikes--;
                var jokeToRate = new UserJokeDislikes(ratingModel.JokeId, user);
                this.context.UsersJokeDislikes.Add(jokeToRate);

                if (IsUserLikedJoke(ratingModel.JokeId, user.Id))
                {
                    var likedJoke = this.context.UsersJokeLikes.FirstOrDefault(x =>
                        x.User.UserName == ratingModel.Username && x.JokeId == ratingModel.JokeId);
                    this.context.UsersJokeLikes.Remove(likedJoke);
                    this.context.SaveChanges();
                }
            }

            this.context.SaveChanges();
            return joke;
        }

        private bool IsUserDisLikedJoke(int jokeId, string userId)
        {
            var result = this.context.UsersJokeDislikes.Any(x => x.JokeId == jokeId && x.UserId == userId);
            return result;
        }

        private bool IsUserLikedJoke(int jokeId, string userId)
        {
            var result = this.context.UsersJokeLikes.Any(x => x.JokeId == jokeId && x.UserId == userId);
            return result;
        }

        public IList<int> GetUserLikedJokesIdList(string userId)
        {
            var ids = this.context.UsersJokeLikes.Where(x => x.UserId == userId).Select(x=>x.JokeId).ToList();
            return ids;
        }

        public IList<int> GetUserDisikedJokesIdList(string userId)
        {
            var ids = this.context.UsersJokeDislikes.Where(x => x.UserId == userId).Select(x=>x.JokeId).ToList();
            return ids;
        }
    }
}