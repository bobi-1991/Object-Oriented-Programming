using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    public interface IStory : ITaskBase, IAssigneable
    {
        Priority Priority { get; }
        StorySize Size { get; }
        StoryStatus Status { get; }
        public string AdvanceStatus();
        public string RevertStatus();
        public string AdvanceSize();
        public string RevertSize();
        public string AdvancePriority();
        public string RevertPriority();
    }
}
