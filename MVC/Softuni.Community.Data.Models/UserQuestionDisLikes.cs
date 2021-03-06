﻿namespace Softuni.Community.Data.Models
{
    public class UserQuestionDisLikes
    {
        public UserQuestionDisLikes() { }
        public UserQuestionDisLikes(int questionId, CustomUser user)
        {
            this.QuestionId = questionId;
            this.User = user;
        }

        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string UserId { get; set; }
        public CustomUser User { get; set; }
    }
}
