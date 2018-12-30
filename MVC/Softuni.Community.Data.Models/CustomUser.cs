﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Softuni.Community.Data.Models
{
    public class CustomUser : IdentityUser
    {
        public int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public bool IsProfileSettedUp { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Joke> Jokes { get;set;}
        public ICollection<Meme> Memes { get;set;}
    }
}