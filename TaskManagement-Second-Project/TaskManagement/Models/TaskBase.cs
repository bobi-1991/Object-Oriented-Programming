using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public abstract class TaskBase : ICommentable, ITaskBase
    {
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 50;
        public const string TitleErrorMessage = "Title cannot be less than {0} and more than {1} symbols";
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;
        public const string DescriptionErrorMessage = "Description cannot be less than {0} and more than {1} symbols";
        public const string NullOrWhiteSpaceErrorMessage = "Incorrect data entered (The imput cannot be empty)! Please try again";
        public const string timeFormat = "yyyy/MM/dd|HH:mm:ss.ffff";

        private string title;
        private string description;
        private static int id = 0;
        private readonly IList<string> history = new List<string>();
        private readonly IList<IComment> comments = new List<IComment>();
        private TaskType tasktype;
        protected TaskBase(string title, string description, TaskType taskType)
        {
            id++;
            Id = id;
            Title = title;
            Description = description;
            TaskType = taskType;
            this.AddToHistory($"[{this.Time.ToString(timeFormat)}] Task {GetType().Name} was created.");
        }

        public TaskType TaskType
        {
            get { return tasktype; }
            set { tasktype = value; }
        }

        public string Title
        {
            get { return this.title; }
            private set
            {
                Validator.ValidateIsNotNullOrWhiteSpace(value, NullOrWhiteSpaceErrorMessage);
                Validator.ValidateIntRange(value.Length, TitleMinLength, TitleMaxLength, TitleErrorMessage);
                this.title = value;
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                Validator.ValidateIsNotNullOrWhiteSpace(value, NullOrWhiteSpaceErrorMessage);
                Validator.ValidateIntRange(value.Length, DescriptionMinLength, DescriptionMaxLength, DescriptionErrorMessage);
                this.description = value;
            }
        }

        public IList<string> History
        {
            get { return this.history.ToList(); }
        }

        public IList<IComment> Comments
        {
            get { return this.comments.ToList(); }
        }

        public int Id { get; }

        public DateTime Time { get => DateTime.Now; }

        public void AddComment(IComment comment)
        {
            comments.Add(comment);
            this.AddToHistory($"[{this.Time.ToString(timeFormat)}] {comment.Author} add comment to {this.GetType().Name}: {comment.Content}");
        }

        public string ViewHistory()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in history)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Task: {GetType().Name}");
            sb.AppendLine($"ID: {this.Id}");
            sb.AppendLine($"Title: {this.Title}");
            
            return sb.ToString().Trim();
        }

        protected void AddToHistory(string description)
        {
            this.history.Add(description);
        }
    }
}
