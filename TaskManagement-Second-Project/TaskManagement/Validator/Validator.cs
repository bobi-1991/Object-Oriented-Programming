namespace TaskManagement
{
    using System;
    using System.Text.RegularExpressions;
    using TaskManagement.Core;
    using TaskManagement.Exceptions;

    public static class Validator
    {
        public static void ValidateIntRange(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                string errorMsg = string.Format(message, min, max);
                throw new InvalidUserInputException(errorMsg);
            }
        }
        public static void ValidateIntegerRange(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                string errorMsg = string.Format(message, min, max);
                throw new InvalidUserInputException(errorMsg);
            }
        }
        public static void ValidateIsNotNullOrWhiteSpace(string arg, string message)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new InvalidUserInputException(message);
            }
        }
    }
}
