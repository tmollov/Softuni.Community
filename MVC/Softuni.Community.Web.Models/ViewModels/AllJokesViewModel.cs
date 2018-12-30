using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class AllJokesViewModel
    {
        public IList<JokeViewModel> Jokes { get; set; }

        public IList<int> UserLikedJokes { get;set;}
        public IList<int> UserDislikedJokes { get;set;}
    }
}
