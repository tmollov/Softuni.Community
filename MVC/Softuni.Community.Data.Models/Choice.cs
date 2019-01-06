using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class Choice
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int GameProblemId { get; set; }
        public GameProblem GameProblem { get; set; }
    }
}