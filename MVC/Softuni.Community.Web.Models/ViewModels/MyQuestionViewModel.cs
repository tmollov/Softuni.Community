using System;
using System.Collections.Generic;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Web.Models.ViewModels
{
    public class MyQuestionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }

        public Category Category { get; set; }

        public int AnswerCount { get; set; }
    }
}