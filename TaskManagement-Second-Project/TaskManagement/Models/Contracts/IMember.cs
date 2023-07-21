using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface IMember:IActivityHistory,ITime
    {
        string Name { get; }
        IList<ITaskBase> Tasks { get; }
        public void AddComment(IComment commentToAdd, ITaskBase taskToAddComment);

    }
}
