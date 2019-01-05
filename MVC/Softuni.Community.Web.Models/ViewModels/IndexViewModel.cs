using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class IndexViewModel
    {
        public IList<JokeViewModel> Jokes { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }
    }
}