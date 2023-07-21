using System;

namespace BoardR
{
    public class Task : BoardItem
    {
        public const int MinLengthAssignee = 5;
        public const int MaxLengthAssignee = 30;
        private string assignee;

        public Task(string title, string assignee, DateTime dueDate) : base(title, dueDate, Status.ToDo)
        {
            Assignee = assignee;
            this.status = Status.ToDo;
            this.logs.Add(new EventLog(this.ViewInfo()));
        }

        public string Assignee
        {
            get
            {
                return this.assignee;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Assignee cannot be null or empty.");
                }
                if (value.Length < MinLengthAssignee || value.Length > MaxLengthAssignee)
                {
                    throw new ArgumentException("Assignee cannot be less than 5 and more than 30");
                }
                if (!string.IsNullOrEmpty(this.assignee) && this.assignee != value)
                {
                    this.logs.Add(new EventLog
                        ($"Assignee changed from {assignee} to {value}"));
                }
                this.assignee = value;
            }
        }

        public override string ViewInfo()
        {
            //Task: 'Write unit tests', [Todo|19-06-2020] Assignee: Pesho
            string formatDate = dueDate.ToString("dd-M-yyyy");
            return $"Task: '{title}', [{status}|{formatDate}] Assignee: {assignee}";
        }

        public override void AdvanceStatus()
        {
            //Todo -> InProgress -> Done -> Verified
            if (this.status == Status.Verified)
            {
                this.logs.Add(new EventLog
                    ("Task status already at Verified"));
            }
            if (this.status != Status.Verified)
            {
                this.status += 1;
                this.logs.Add(new EventLog
                    ($"Task changed from {this.status - 1} to {this.status}"));
            }
        }

        public override void RevertStatus()
        {
            //Open <-> Todo <-> InProgress <-> Done <-> Verified
            if (this.status == Status.ToDo)
            {
                this.logs.Add(new EventLog
                    ("Task status already ToDo"));
            }
            if (this.status != Status.ToDo)
            {
                this.status -= 1;
                this.logs.Add(new EventLog
                    ($"Task changed from {this.status + 1} to {this.status}"));
            }
        }
    }
}
