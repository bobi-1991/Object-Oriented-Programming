using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public class Bug : TaskBase, IBug
    {
        private IList<string> steps;
        private Priority priority;
        private BugSeverity severity;
        private BugStatus status;
        private IMember assignee;

        public Bug(string title, string description, Priority priority, BugSeverity severity, List<string> steps)
            : base(title, description, TaskType.Bug)
        {
           this.status = BugStatus.Active;
            Priority = priority;
            Severity = severity;
            Steps = steps; 
        }

        public IList<string> Steps
        {
            get { return steps; }
            private set { this.steps = value; }
        }

        public BugStatus Status
        {
            get { return this.status; }
            private set { this.status = value; }
        }

        public Priority Priority
        {
            get { return this.priority; }
            private set { this.priority = value; }
        }
        public BugSeverity Severity
        {
            get { return this.severity; }
            private set { this.severity = value; }
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

        public string AdvancePriority()
        {
            if (this.priority != Priority.High)
            {
                var prev = this.priority;
                this.Priority++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Priority changed from {prev} to {this.Severity}");
                return $"Priority changed from {prev} to {this.Priority}";
            }
            else
            {
                return "Priority already High";
            }
        }
        public string RevertPriority()
        {
            if (this.priority != Priority.Low)
            {
                var prev = this.priority;
                this.Priority--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Priority changed from {prev} to {this.Severity}");
                return $"Priority changed from {prev} to {this.Priority}";
            }
            else
            {
                return "Priority already Low";
            }
        }

        public string AdvanceSeverity()
        {
            if (this.severity != BugSeverity.Critical)
            {
                var prev = this.severity;
                this.Severity++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Severity changed from {prev} to {this.Severity}");
                return $"Severity changed from {prev} to {this.Severity}";
            }
            else
            {
                return "Severity already Critical";
            }
        }
        public string RevertSeverity()
        {
            if (this.severity != BugSeverity.Minor)
            {
                var prev = this.severity;
                this.Severity--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Severity changed from {prev} to {this.Severity}");
                return $"Severity changed from {prev} to {this.Severity}";
            }
            else
            {
                return "Severity already Minor";
            }
        }

        public string AdvanceStatus()
        {
            if (this.Status != BugStatus.Fixed)
            {
                var prev = this.Status;
                this.Status++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status}");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Fixed";
            }
        }
        public string RevertStatus()
        {
            if (this.Status != BugStatus.Active)
            {
                var prev = this.Status;
                this.Status--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status}");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Active";
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Priority: {this.Priority}");
            sb.AppendLine($"Severity: {this.Severity}");
            sb.Append($"Status: {this.Status}");

            return sb.ToString();
        }
    }
}
