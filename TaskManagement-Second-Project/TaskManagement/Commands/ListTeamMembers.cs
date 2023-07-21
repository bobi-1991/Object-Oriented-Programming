using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class ListTeamMembers : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;
        public ListTeamMembers(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string teamName = this.CommandParameters[0];

            return this.ShowTeamMembers(teamName);
        }

        private string ShowTeamMembers(string teamName)
        {
            var team = base.Repository.GetTeam(teamName);

            StringBuilder sb = new StringBuilder();

            if (team.Members.Count == 0)
            {
                sb.AppendLine("Team Member's list is empty");
            }
            else
            {
                int counter = 1;
                sb.AppendLine($"--{teamName} Members--");

                foreach (var member in team.Members)
                {
                    sb.AppendLine($"{counter}. {member.Name}");
                    counter++;
                }
            }
            return sb.ToString();
        }
    }
}
