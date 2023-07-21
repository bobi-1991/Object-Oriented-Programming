using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ChangeFeedbackStatusCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public ChangeFeedbackStatusCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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

            return this.ChangeStatus(id, direction);
        }
        private string ChangeStatus(int id, string direction)
        {
            var feedback = this.Repository.GetFeedback(id);

            switch (direction)
            {
                case "advance":
                    return feedback.AdvanceStatus();
                case "revert":
                    return feedback.RevertStatus();
                default:
                    throw new InvalidUserInputException($"Direction: {direction} doesn't exist!");
            }

        }
    }
}
