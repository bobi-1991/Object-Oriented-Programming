using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums;

namespace TaskManagement.Commands
{
    public class CreateStoryCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 6;

        public CreateStoryCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string title = CommandParameters[0];
            string description = CommandParameters[1];
            Priority priority = base.ParsePriorityParameter(CommandParameters[2], "priority");
            StorySize size = this.ParseStorySizeParameter(CommandParameters[3], "size");
            string boardName = CommandParameters[4];
            string teamName = CommandParameters[5];

            return this.CreateStory(title, description, priority, size, boardName, teamName);
        }

        private string CreateStory(string title, string description, Priority priority, StorySize size,string boardName, string teamName)
        {
            if (!base.Repository.TeamExist(teamName))
            {
                throw new InvalidUserInputException($"Team name {teamName} is not exist");
            }
            if (!this.Repository.BoardExistInTeam(teamName, boardName))
            {
                throw new InvalidUserInputException($"Board {boardName} not exist in {teamName}.");
            }
            base.Repository.CreateStory(title, description, priority, size, boardName, teamName);

            var team = base.Repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            var currentStory = board.Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            int storyID = currentStory.Id;

            return $"Story with title {title} and ID {storyID} was created";
        }

        private StorySize ParseStorySizeParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out StorySize result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Large, Medium, Small");
        }

    }
}
