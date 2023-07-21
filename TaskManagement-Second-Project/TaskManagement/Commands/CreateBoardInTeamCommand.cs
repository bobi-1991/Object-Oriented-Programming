using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class CreateBoardInTeamCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public CreateBoardInTeamCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string boardName = CommandParameters[0];
            string teamName = CommandParameters[1];

            return this.CreateBoardInTeam(boardName, teamName);

        }
        private string CreateBoardInTeam(string boardName, string teamName)
        {

            if (this.Repository.TeamExist(teamName) == false)
            {
                string errorMessage = $"Team with name {teamName} does not exist.";
                throw new EntityNotFoundException(errorMessage);
            }

            base.Repository.CreateBoardInTeam(boardName, teamName);

            return $"Board with name {boardName} was created in {teamName} team.";
        }
    }
}
