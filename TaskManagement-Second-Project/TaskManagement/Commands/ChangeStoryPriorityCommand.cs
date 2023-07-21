using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ChangeStoryPriorityCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public ChangeStoryPriorityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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
            var story = this.Repository.GetStory(id);

            switch (direction)
            {
                case "advance":
                    return story.AdvancePriority();
                case "revert":
                    return story.RevertPriority();
                default:
                    throw new InvalidUserInputException($"Direction: {direction} doesn't exist!");
            }

        }
    }
}
