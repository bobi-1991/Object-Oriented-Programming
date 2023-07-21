using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    public interface IFeedback : ITaskBase
    {
        public int Rating { get; }
        FeedBackStatus Status { get; }
        void ChangeRating(int rating);
        public string AdvanceStatus();

        public string RevertStatus();
    }
}
