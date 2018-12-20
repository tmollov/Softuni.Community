namespace Softuni.Community.Data.Models
{
    public class QuestionsTags
    {
        public QuestionsTags(){}
        public QuestionsTags(Question question, Tag tag)
        {
            this.Question = question;
            this.Tag = tag;
        }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
