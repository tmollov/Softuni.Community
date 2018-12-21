namespace Softuni.Community.Data.Models
{
    public class UserAnswerDisLikes
    {
        public UserAnswerDisLikes() { }
        public UserAnswerDisLikes(int answerId, CustomUser user)
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
