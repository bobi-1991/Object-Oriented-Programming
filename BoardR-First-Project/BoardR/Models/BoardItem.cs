using System;
using System.Collections.Generic;
using System.Text;

namespace BoardR
{
    public abstract class BoardItem
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 30;
        private protected string title;
        private protected DateTime dueDate;
        private protected Status status;
        public readonly List<EventLog> logs = new List<EventLog>();

        public BoardItem(string title, DateTime dueDate, Status status = Status.Open)
        {
            Title = title;
            DueDate = dueDate;
            this.status = Status.Open;
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title cannot be null or empty.");
                }
                if (value.Length < TitleMinLength || value.Length > TitleMaxLength)
                {
                    throw new ArgumentException("Title is outside the requirements for Length");
                }
                if (!string.IsNullOrEmpty(this.title) && this.title != value)
                {
                    this.logs.Add(new EventLog
                        ($"Title changed from {title} to {value}"));
                }
                this.title = value;
            }
        }

        public DateTime DueDate
        {
            get
            {
                return this.dueDate;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("Date must be in the future");
                }
                if (this.dueDate < value && this.dueDate != new DateTime())
                {
                    //DueDate changed from '25-01-2020' to '25-01-2030'
                    this.logs.Add(new EventLog
                        ($"DueDate changed from '{dueDate.ToString("dd-M-yyyy")}' to '{value.ToString("dd-M-yyyy")}'"));
                }
                this.dueDate = value;
            }
        }

        public Status Status
        {
            get
            {
                return this.status;
            }
        }

        public abstract void RevertStatus();

        public abstract void AdvanceStatus();

        public string ViewHistory()
        {
            var str = new StringBuilder();
            foreach (var item in this.logs)
            {
                str.AppendLine($"[{item.Time.ToString("yyyyMMdd|HH:mm:ss.ffff")}]{item.Description}");

            }
            return str.ToString().Trim();
        }

        public virtual string ViewInfo()
        {
            string formatDate = dueDate.ToString("dd-M-yyyy");
            return $"Item created: '{title}', [{status}|{formatDate}]";
        }
    }
}
