using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Exceptions
{
    internal class DuplicateEntityException : ApplicationException
    {
        public DuplicateEntityException(string message) : base(message)
        {
        }
    }
}
