using System;

namespace BoardR
{
    public class Issue : BoardItem
    {
        private readonly string description;

        public Issue(string title, string description, DateTime dueDate, Status status = Status.Open)
            : base(title, dueDate, status)
        {
            //Coupling = how much a class depends on other class = as low as possible
            //Null Coalesinc operator
            //Description = description ?? "No description";
            this.status = Status.Open;
            this.description = description is null ? "No description" : description;
            this.logs.Add(new EventLog(this.ViewInfo()));
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public override string ViewInfo()
        {
            string formatDate = dueDate.ToString("dd-M-yyyy");
            return $"Issue: '{title}', [{status}|{formatDate}]. Description: {description}";
        }

        public override void AdvanceStatus()
        {
            if (this.status == Status.Verified)
            {
                this.logs.Add(new EventLog 
                    ("Issue status already Verified"));
            }
            if (this.status == Status.Open)
            {
                this.status = Status.Verified;
                this.logs.Add(new EventLog
                    ($"Issue status set to {this.status}"));
            }
        }

        public override void RevertStatus()
        {
            //Open <-> Todo <-> InProgress <-> Done <-> Verified
            if (this.status == Status.Open)
            {
                this.logs.Add(new EventLog
                    ("Issue status already Open"));
            }
            if (this.status == Status.Verified)
            {
                this.status = Status.Open;
                this.logs.Add(new EventLog
                    ($"Issue status set to {this.status}"));
            }
        }
    }
}
