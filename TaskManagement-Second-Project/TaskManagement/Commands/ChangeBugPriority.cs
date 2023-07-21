using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ChangeBugPriorityCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public ChangeBugPriorityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            int id = ParseIntParameter(CommandParameters[0], "id");
            string direction = CommandParameters[1];

            return this.ChangePriority(id, direction);
        }
        private string ChangePriority(int id, string direction)
        {
            var bug = this.Repository.GetBug(id);

            switch (direction)
            {
                case "advance":
                    return bug.AdvancePriority();
                case "revert":
                    return bug.RevertPriority();
                default:
                    throw new InvalidUserInputException($"Direction: {direction} doesn't exist!");
            }

        }
    }
}
