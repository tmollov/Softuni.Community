﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Softuni.Community.Data.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

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