using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Models.Enums;

namespace Softuni.Community.Data.Models
{
    public class Question
    {
        public Question(){}
        public Question(string title , string content,Category category, CustomUser publisher)
        {
            this.Title = title;
            this.Content = content;
            this.PublishTime = DateTime.Now;
            this.Category = category;
            this.Publisher = publisher;
        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }
        
        public Category Category { get; set; }

        public string PublisherId { get; set; }
        public CustomUser Publisher { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}