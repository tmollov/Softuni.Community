using System;
using System.Collections.Generic;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionId {get; set; }

        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }
        
        public Category Category { get; set; }

        public string PublisherName { get; set; }

        public string PublisherPicture { get; set; }

        public IList<string> Tags { get; set; }

        public int AnswerCount { get; set; }
    }
}
