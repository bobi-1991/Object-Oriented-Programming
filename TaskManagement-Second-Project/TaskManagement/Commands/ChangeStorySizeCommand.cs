using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ChangeStorySizeCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public ChangeStorySizeCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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

            return this.ChangeSize(id, direction);
        }

        private string ChangeSize(int id, string direction)
        {
            var story = this.Repository.GetStory(id);

            switch (direction)
            {
                case "advance":
                    return story.AdvanceSize();
                case "revert":
                    return story.RevertSize();
                default:
                    throw new InvalidUserInputException($"Direction: {direction} doesn't exist!");
            }

        }

    }
}