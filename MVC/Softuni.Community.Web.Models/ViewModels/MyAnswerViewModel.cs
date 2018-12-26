using System;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class MyAnswerViewModel
    {
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }

        public int QuestionId { get; set; }

        public string QuestionTitle { get; set; }
    }
}
