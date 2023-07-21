using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface IBoard:IActivityHistory,ITime
    {
        string Name { get; }
        IList<ITaskBase> Tasks { get; }
        void AddBug(ITaskBase bugTask);
        void AddStory(ITaskBase storyTask);
        void AddFeedback(ITaskBase feedbackTask);

    }
}
