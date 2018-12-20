using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class Answer
    {
        public Answer(){}
        public Answer(string content, CustomUser publisher, Question question)
        {
            this.Content = content;
            this.Publisher = publisher;
            this.Question = question;
            this.PublishTime = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }

        public string PublisherId { get; set; }
        public CustomUser Publisher { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}