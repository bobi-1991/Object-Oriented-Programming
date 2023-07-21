using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class AddMemberToTeamCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public AddMemberToTeamCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string memberName = this.CommandParameters[0];
            string teamName = this.CommandParameters[1];

            return this.AddMemberToTeam(memberName, teamName);
        }

        private string AddMemberToTeam(string memberName, string teamName)
        {
            if (this.Repository.MemberExist(memberName) == false)
            {
                string errorMessage = $"Member with name {memberName} does not exist.";
                throw new EntityNotFoundException(errorMessage);
            }
            if (this.Repository.TeamExist(teamName) == false)
            {
                string errorMessage = $"Team with name {teamName} does not exist.";
                throw new EntityNotFoundException(errorMessage);
            }
            
            base.Repository.AddMemberToTeam(memberName,teamName);

            return $"Member with name {memberName} was added to {teamName} team.";
        }
    }
}
