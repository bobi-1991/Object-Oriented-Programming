using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ShowHistoryInTaskCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public ShowHistoryInTaskCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            int id = this.ParseIntParameter(CommandParameters[0], "id");

            return this.ShowHistoryInTask(id);
        }
        private string ShowHistoryInTask(int id)
        {
            var task = this.Repository.GetTaskBase(id);

            if (task == null)
            {
                throw new InvalidUserInputException($"Task with ID {id} is not exist.");
            }

            return task.ViewHistory();
        }
    }
}
