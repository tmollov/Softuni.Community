using System;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }

        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }

        public string PublisherName { get; set; }

        public string PublisherPicture { get; set; }
    }
}