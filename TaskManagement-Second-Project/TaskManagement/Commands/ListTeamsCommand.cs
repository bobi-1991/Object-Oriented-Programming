using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core.Contracts;


namespace TaskManagement.Commands
{
    public class ListTeamsCommand : BaseCommand
    {
        public ListTeamsCommand(IRepository repository)
            : base(repository)
        {
        }

        public override string Execute()
        {
            return ShowTeams();
        }

        private string ShowTeams()
        {
            StringBuilder sb = new StringBuilder();

            var teams = this.Repository.Teams;

            if (teams.Count == 0)
            {
                sb.AppendLine("Team list are empty");
            }
            else
            {
                int counter = 1;
                sb.AppendLine("--Teams--");

                foreach (var team in teams)
                {
                    sb.AppendLine($"{counter}. {team.Name}");
                    counter++;
                }
            }

            return sb.ToString();
        }
    }
}
