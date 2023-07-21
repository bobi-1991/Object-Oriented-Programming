using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public class Story : TaskBase, IStory
    {
        private Priority priority;
        private StorySize size;
        private StoryStatus status;
        private IMember assignee;

        public Story(string title, string description, Priority priority, StorySize size)
            : base(title, description, TaskType.Story)
        {
            Priority = priority;
            Size = size;
            this.status = StoryStatus.NotDone;
        }

        public Priority Priority
        {
            get { return this.priority; }
          private  set { this.priority = value; }
        }

        public StorySize Size
        {
            get { return this.size; }
           private set { this.size = value; }
        }

        public StoryStatus Status
        {
            get { return this.status; }
           private set { this.status = value; }
        }
        public IMember Assignee
        {
            get { return this.assignee; }
           private set { this.assignee = value; }
        }

        public void AddAssignee(IMember member)
        {
            this.Assignee = member;
            base.AddToHistory($"[{this.Time.ToString(timeFormat)}] {this.GetType().Name} was assign to {this.Assignee.Name}.");
        }

        public void Unassign()
        {
            base.AddToHistory($"[{this.Time.ToString(timeFormat)}] {this.GetType().Name} was unassign from {this.Assignee.Name}");
            this.Assignee = null;
        }

        public string AdvanceStatus()
        {
            if (this.Status != StoryStatus.Done)
            {
                var prev = this.Status;
                this.Status++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status}.");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Done";
            }
        }
        public string RevertStatus()
        {
            if (this.Status != StoryStatus.NotDone)
            {
                var prev = this.Status;
                this.Status--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status}.");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Not Done";
            }
        }

        public string AdvanceSize()
        {
            if (this.Size != StorySize.Large)
            {
                var prev = this.Size;
                this.Size++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Size changed from {prev} to {this.Size}.");
                return $"Size changed from {prev} to {this.Size}";
            }
            else
            {
                return "Size cannot be greater than Large";
            }
        }
        public string RevertSize()
        {
            if (this.Size != StorySize.Small)
            {
                var prev = this.Size;
                this.Size--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Size changed from {prev} to {this.Size}.");
                return $"Size changed from {prev} to {this.Size}";
            }
            else
            {
                return "Size cannot be less than Small";
            }
        }
        public string AdvancePriority()
        {
            if (this.Priority != Priority.High)
            {
                var prev = this.Priority;
                this.Priority++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Priority changed from {prev} to {this.Priority}.");
                return $"Priority changed from {prev} to {this.Priority}";
            }
            else
            {
                return "Priority cannot be higher than High";
            }
        }
        public string RevertPriority()
        {
            if (this.Priority != Priority.Low)
            {
                var prev = this.Priority;
                this.Priority--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Priority changed from {prev} to {this.Priority}.");
                return $"Priority changed from {prev} to {this.Priority}";
            }
            else
            {
                return "Priority cannot be lower than Low";
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Priority: {this.Priority}");
            sb.AppendLine($"Size: {this.Size}");
            sb.Append($"Status: {this.Status}");

            return sb.ToString();
        }
    }
}
