using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class Meme
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public string PublisherId { get; set; }
        public CustomUser Publisher { get; set; }
    }
}
