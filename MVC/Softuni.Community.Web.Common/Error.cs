namespace Softuni.Community.Web.Common
{
    public class Error
    {
        public const string IdRequired = "ID is required!";

        public const string TitleRequired = "Title is required!";
        public const string TitleLength = "Title must be at least {2} and at max {1} characters long.";
        
        public const string QuestionCannotBeEmpty = "Your Question cannot be empty!";
        public const string QuestionLength = "Your Question must be at least {2} and at max {1} characters long.";

        public const string UsernameLength = "Your Username must be at least {2} and at max {1} characters long.";
        public const string UsernameRequired = "Username is required";

        public const string PasswordLength = "Your Password must be at least {2} and at max {1} characters long.";
        public const string PasswordsDoesntMatch = "The password and confirmation password do not match.";
        public const string PasswordNotValid = "Wrong password!";
        public const string PasswordRequired = "Password is required!";
        public const string ConfirmPasswordRequired = "Confirm password is required!";

        public const string UserWithGivenEmailNotFound = "User with given email not found.";
        public const string EmailRequired = "Email is required!";
        
        public const string ProblemContentRequired = "Problem description is required!";
        public const string AnswerIDRequired = "Answer ID is required!";
        public const string AnswerRequired = "Answer is required!";
        public const string QuestionIDRequired = "Question ID is required!";
        public const string RightAnswerRequired = "Right answer is required!";

        public const string CategoryRequired = "Category is required!";

        public const string RatingRequired = "Rating is required";

        public const string JokeContentRequired = "Joke desciption is required!";
        public const string JokeLength = "Joke must be at least {2} and at max {1} characters long.";
        public const string JokeIDRequired = "Joke ID is required!";

        public const string FirstNameLength = "First name must be at least {2} and at max {1} characters long.";
        public const string AboutMeLength = "About me must be at least {2} and at max {1} characters long.";
        public const string StateLength = "State must be at least {2} and at max {1} characters long.";

    }
}