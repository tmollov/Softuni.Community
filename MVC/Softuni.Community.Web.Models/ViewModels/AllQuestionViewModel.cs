using System.Collections.Generic;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class AllQuestionViewModel
    {
        public AllQuestionViewModel() { }
        public AllQuestionViewModel(ICollection<QuestionViewModel> viewModels)
        {
            this.ViewModels = viewModels;
        }
        public ICollection<QuestionViewModel> ViewModels { get; set; }
    }
}