using System;

namespace BoardR
{
    public class EventLog
    {
        private string description;
        private DateTime time;

        public EventLog(string description)
        {
            Description = description;
            Time = time;
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(value);
                }
                this.description = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                
                this.time = DateTime.Now;
            }
        }

        public string ViewInfo()
        {
            string formatDate = Time.ToString("yyyyMMdd|HH:mm:ss.ffff");
            return $"[{formatDate}]{description}";
        }
    }
}
