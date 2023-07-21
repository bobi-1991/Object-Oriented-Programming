using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface IActivityHistory
    {
        IList<string> ActivityHistory { get; }
        string ShowActivityHistory();
        void AddActivity(string description);
    }
}
