using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Commands.Contracts
{
    public interface ICommand
    {
        string Execute();
    }
}
