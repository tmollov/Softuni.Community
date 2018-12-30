using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Data.Models
{
    public class Joke
    {
        public Joke(){}
        public Joke(string content, CustomUser publisher)
        {
            this.Content = content;
            this.Publisher = publisher;
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public JokeCategory Category { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public string PublisherId { get; set; }
        public CustomUser Publisher { get; set; }
    }
}