using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface IAssigneable
    {
        IMember Assignee { get; }
        void AddAssignee(IMember member);
        void Unassign();
    }
}
