namespace Softuni.Community.Data.Models
{
    public class Tag
    {
        public Tag(){}
        public Tag(string name, Question question)
        {
            this.Name = name;
            this.Question = question;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}