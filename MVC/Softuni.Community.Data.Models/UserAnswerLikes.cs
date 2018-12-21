namespace Softuni.Community.Data.Models
{
    public class UserAnswerLikes
    {
        public UserAnswerLikes() { }
        public UserAnswerLikes(int answerId, CustomUser user)
        {
            this.AnswerId = answerId;
            this.User = user;
        }

        public int Id { get; set; }

        public int AnswerId { get; set; }

        public string UserId { get; set; }
        public CustomUser User { get; set; }
    }
}