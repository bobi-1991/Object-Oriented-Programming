using System;

namespace TaskManagement.Exceptions
{
    public class AuthorizationException : ApplicationException
    {
        public AuthorizationException(string message)
            : base(message)
        {
        }
    }
}

