namespace Softuni.Community.Web.Common
{
    public class Required
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 6;

        public const int QuestionMaxLength = 2000;
        public const int QuestionMinLength = 6;

        public const int UsernameMaxLength = 24;
        public const int UsernameMinLength = 6;

        public const int PasswordMaxLength = 50;
        public const int PasswordMinLength = 6;

        public const int AnswerContentMaxLength = 200;
        public const int AnswerContentMinLength = 12;

        public const int JokeContentMaxLength = 500;
        public const int JokeContentMinLength = 12;

        public const int IdMin = 1;
        public const int IdMax = int.MaxValue;

        public const int RatingMin = -1;
        public const int RatingMax = 1;

        public const int NameMin = 4;
        public const int NameMax = 20;

        public const string FirstLastNameRegex = "^[A-Za-z]+$";

        public const int AboutMeMin = 0;
        public const int AboutMeMax = 300;

        public const int StateMax = 30;
        public const int StateMin = 4;
        

    }
}