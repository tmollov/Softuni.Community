namespace Softuni.Community.Data.Models
{
    public class QuestionsCategories
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
