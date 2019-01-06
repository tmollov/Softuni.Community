using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class ProblemDetailsViewModel
    {
        public int Id { get; set; }

        public string ProblemContent { get; set; }

        public string RightAnswer { get; set; }

        public IList<string> Answers { get; set; }
    }
}
