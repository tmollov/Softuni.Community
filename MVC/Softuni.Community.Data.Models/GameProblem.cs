using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Data.Models
{
    public class GameProblem
    {
        public int Id { get; set; }

        [Required]
        public string ProblemContent { get; set; }

        [Required]
        public IList<Choice> Choices { get; set; }

        [Required]
        public string RightAnswer { get; set; }
    }
}
