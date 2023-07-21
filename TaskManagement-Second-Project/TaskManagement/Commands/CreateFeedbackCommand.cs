using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums;

namespace TaskManagement.Commands
{
    public class CreateFeedbackCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 5;

        public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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
            int rating = int.Parse(CommandParameters[2]);
            string boardName = CommandParameters[3];
            string teamName = CommandParameters[4];


            return this.CreateFeedback(title, description, rating, boardName, teamName);

        }

        private string CreateFeedback(string title, string description, int rating, string boardName, string teamName)
        {
            if (!base.Repository.TeamExist(teamName))
            {
                throw new InvalidUserInputException($"Team name {teamName} is not exist");
            }
            if (!this.Repository.BoardExistInTeam(teamName, boardName))
            {
                throw new InvalidUserInputException($"Board {boardName} not exist in {teamName}.");
            }
            base.Repository.CreateFeedback(title, description, rating, boardName, teamName);

            var team = base.Repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            var currentFeedback = board.Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            int feedbackID = currentFeedback.Id;

            return $"Feedback with title {title} and ID {feedbackID} was created";
        }

    }
}
