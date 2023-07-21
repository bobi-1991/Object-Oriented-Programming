using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Board : IBoard
    {
        public const int BoardNameMinLength = 5;
        public const int BoardNameMaxLength = 10;
        private const string BoardNameLengthError = "Name should be between {0} and {1} symbols.";
        public const string NullOrWhiteSpaceErrorMessage = "Incorrect data entered (The imput cannot be empty)! Please try again";
        public const string timeFormat = "yyyy/MM/dd|HH:mm:ss.ffff";


        private string name;
        private readonly IList<ITaskBase> tasks = new List<ITaskBase>();
        private readonly IList<string> activityHistory = new List<string>();
        public Board(string name)
        {
            Name = name;
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] Board {this.Name} was created.");
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                Validator.ValidateIsNotNullOrWhiteSpace(value, NullOrWhiteSpaceErrorMessage);
                Validator.ValidateIntRange(value.Length, BoardNameMinLength, BoardNameMaxLength, BoardNameLengthError);
                this.name = value;
            }
        }

        public IList<ITaskBase> Tasks
        {
          //  get { return this.tasks; }
          get=> new List<ITaskBase>(tasks);
        }
        public IList<string> ActivityHistory { get => new List<string>(activityHistory); }

        public DateTime Time { get => DateTime.Now; }

        public void AddBug(ITaskBase bugTask)
        {
            this.tasks.Add(bugTask);
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] {this.Name} was added a bug with ID:{bugTask.Id}, Title:{bugTask.Title}.");
        }
        public void AddStory(ITaskBase storyTask)
        {
            this.tasks.Add(storyTask);
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] {this.Name} was added a story with ID:{storyTask.Id}, Title:{storyTask.Title}.");
        }
        public void AddFeedback(ITaskBase feedbackTask)
        {
            this.AddActivity($"[{this.Time.ToString(timeFormat)}] {this.Name} was added a feedback with ID:{feedbackTask.Id}, Title:{feedbackTask.Title}.");
            this.tasks.Add(feedbackTask);
        }

        public void AddActivity(string description)
        {
            this.activityHistory.Add(description);
        }

        public string ShowActivityHistory()
        {
            return string.Join(Environment.NewLine, this.activityHistory.Select(x => x));
        }
        //public void RemoveBug(IBug bugTask)
        //{
        //    this.tasks.Remove(bugTask);
        //}
        //public void RemoveStory(IStory storyTask)
        //{
        //    this.tasks.Remove(storyTask);
        //}
        //public void RemoveFeedback(IFeedback feedbackTask)
        //{
        //    this.tasks.Remove(feedbackTask);
        //}
        //public ITaskBase GetTaskById(int id)
        //{
        //    ITaskBase task = this.tasks.Where(t => t.Id == id).FirstOrDefault();
        //    if (task == null)
        //    {
        //        throw new EntryPointNotFoundException(string.Format(TaskNotFoundError, "Id", id));
        //    }
        //    return task;
        //}
    }
}
