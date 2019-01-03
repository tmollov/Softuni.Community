using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class JokeViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public JokeCategory Category { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public bool IsUserLikedJoke { get; set; }
        public bool IsUserDislikedJoke { get; set; }

        public string Publisher { get; set; }
    }
}