using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Commands.Contracts;
using TaskManagement.Exceptions;
using System.Data;
using TaskManagement.Models.Enums;

namespace TaskManagement.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
          : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> commandParameters, IRepository repository)
        {
            this.CommandParameters = commandParameters;
            this.Repository = repository;
        }

        public abstract string Execute();

        protected IRepository Repository { get; }

        protected IList<string> CommandParameters { get; }

        protected int ParseIntParameter(string value, string parameterName)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be an integer number.");
        }
        protected Priority ParsePriorityParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out Priority result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either High, Medium, or Low.");
        }
    }
}
