using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class UserJokeLikes
    {
        public UserJokeLikes() { }
        public UserJokeLikes(int jokeId, CustomUser user)
        {
            this.JokeId = jokeId;
            this.User = user;
        }

        public int Id { get; set; }

        [Required]
        public int JokeId { get; set; }

        public string UserId { get; set; }
        public CustomUser User { get; set; }
    }
}
