using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public class Feedback : TaskBase, IFeedback
    {
        public const int RatingMinValue = 1;
        public const int RatingMaxValue = 10;
        public const string RatingErrorMessage = "The rating cannot be less than {0} or more than {1}.";

        private int rating;
        private FeedBackStatus status;
        public Feedback(string title, string description, int rating)
            : base(title, description, TaskType.Feedback)
        {
            this.status = FeedBackStatus.New;
            Rating = rating;
        }

        public FeedBackStatus Status
        {
            get { return this.status; }
            private set { this.status = value; }
        }
        public int Rating
        {
            get { return this.rating; }
            private set
            {
                Validator.ValidateIntegerRange(value, RatingMinValue, RatingMaxValue, RatingErrorMessage);
                this.rating = value;
            }
        }

        public string AdvanceStatus()
        {
            if (this.Status != FeedBackStatus.Done)
            {
                var prev = this.Status;
                this.Status++;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status} ");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Done";
            }
        }

        public void ChangeRating(int rating)
        {
            Validator.ValidateIntRange(rating, RatingMinValue, RatingMaxValue, RatingErrorMessage);
            base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Rating changed from {this.Rating} to {rating} ");
            this.Rating = rating;
        }

        public string RevertStatus()
        {
            if (this.Status != FeedBackStatus.New)
            {
                var prev = this.Status;
                this.Status--;
                base.AddToHistory($"[{this.Time.ToString(timeFormat)}] Status changed from {prev} to {this.Status} ");
                return $"Status changed from {prev} to {this.Status}";
            }
            else
            {
                return "Status already Not Done";
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Rating: {this.Rating}");
            sb.Append($"Status: {this.Status}");

            return sb.ToString();
        }

    }
}
