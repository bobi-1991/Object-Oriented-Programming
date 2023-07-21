using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Member : IMember
    {
        public const int MemberNameMinLength = 5;
        public const int MemberNameMaxLength = 15;
        public const string MemberNameErrorMessage = "Member name cannot be less than {0} and more than {1} symbols";
        public const string NullOrWhiteSpaceErrorMessage = "Incorrect data entered (The imput cannot be empty)! Please try again";
        public const string timeFormat = "yyyy/MM/dd|HH:mm:ss.ffff";


        private string name;
        private readonly IList<ITaskBase> tasks = new List<ITaskBase>();
        private readonly IList<string> activityHistory = new List<string>();
        public Member(string name)
        {
            Name = name;
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] Member {this.Name} was created.");
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                Validator.ValidateIsNotNullOrWhiteSpace(value, NullOrWhiteSpaceErrorMessage);
                Validator.ValidateIntRange(value.Length, MemberNameMinLength, MemberNameMaxLength, MemberNameErrorMessage);
                this.name = value;
            }
        }

        public void AddComment(IComment commentToAdd, ITaskBase taskToAddComment)
        {
            taskToAddComment.AddComment(commentToAdd);
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] Member {this.Name} was add a comment to {taskToAddComment.Title}: \n {commentToAdd.Content}.");
        }
        public IList<ITaskBase> Tasks
        {
            get { return this.tasks.ToList(); }
        }

        public IList<string> ActivityHistory
        {
            get { return new List<string>(this.activityHistory); }
        }

        public DateTime Time { get => DateTime.Now; }

        public void AddActivity(string description)
        {
            this.activityHistory.Add(description);
        }

        public string ShowActivityHistory()
        {
            return string.Join(Environment.NewLine, this.activityHistory.Select(x => x));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Member name: {this.Name}");

            return base.ToString();
        }

    }
}
