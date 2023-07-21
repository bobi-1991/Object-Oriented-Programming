using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class CreateTeamCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public CreateTeamCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string name = this.CommandParameters[0];

            return this.CreateTeam(name);
        }
        private string CreateTeam(string teamName)
        {
            if (this.Repository.TeamExist(teamName))
            {
                string errorMessage = $"Team with name {teamName} already exist.";
                throw new ArgumentException(errorMessage);
            }

            base.Repository.CreateTeam(teamName);

            return $"Team with name: {teamName} was created";
        }
    }
}
