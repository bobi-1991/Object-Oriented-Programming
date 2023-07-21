using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    public interface ITaskBase : ICommentable,ITime
    {
        int Id { get; }

        string Title { get; }

        string Description { get; }

        IList<string> History { get; }

        TaskType TaskType { get; }
        string ViewHistory();


    }
}
