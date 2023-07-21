using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    public interface IBug : ITaskBase, IAssigneable
    {
        public Priority Priority { get; }

        public BugSeverity Severity { get; }

        public BugStatus Status { get; }

        IList<string> Steps { get; }

        public string AdvancePriority();

        public string RevertPriority();

        public string AdvanceSeverity();

        public string RevertSeverity();

        public string AdvanceStatus();

        public string RevertStatus();
    }
}
