using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Comment : IComment
    {

        public const int CommentMinLength = 3;
        public const int CommentMaxLength = 200;
        public const string InvalidCommentError = "Content must be between 3 and 200 characters long!";
        public const string NullOrWhiteSpaceErrorMessage = "Incorrect data entered (The imput cannot be empty)! Please try again";


        private string content;
        private string author;
        public Comment(string content, string author)
        {
            Validator.ValidateIntRange(content.Length, CommentMinLength, CommentMaxLength, InvalidCommentError);
            Validator.ValidateIsNotNullOrWhiteSpace(author, NullOrWhiteSpaceErrorMessage);
            Content = content;
            Author = author;
            
        }

        public string Content
        {
            get { return content; }
         private set { this.content = value; }
        }

        public string Author
        {
            get { return author; }
           private set { this.author = value; }
        }
    }
}
