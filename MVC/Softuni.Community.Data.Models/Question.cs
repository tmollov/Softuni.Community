using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }

        public int Rating { get; set; }

        
        public int QuestionsCategoriesId { get; set; }
        public QuestionsCategories QuestionsCategories { get; set; }

        public string PublisherId { get; set; }
        public CustomUser Publisher { get; set; }

        public ICollection<QuestionsTags> Tags { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}