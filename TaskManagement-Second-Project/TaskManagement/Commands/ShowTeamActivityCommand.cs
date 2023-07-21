using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ShowTeamActivityCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;
        public const string DashesSeparator = "------------";

        public ShowTeamActivityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string teamName = CommandParameters[0];

            return this.ShowTeamsActivity(teamName);
        }
        private string ShowTeamsActivity(string teamName)
        {
            StringBuilder sb = new StringBuilder();
            var team = this.Repository.GetTeam(teamName);

            if (team.Boards.Count == 0)
            {
                sb.AppendLine("Boards list are empty");
            }
            else
            {
                int counter = 1;
                sb.AppendLine($"--Team-- {team.Name}:");

                foreach (var board in team.Boards)
                {
                    sb.AppendLine($"{counter}. {board.Name}");
                    sb.AppendLine(board.ShowActivityHistory().ToString());
                    sb.AppendLine($"{DashesSeparator}");
                    counter++;
                }
            }

            return sb.ToString();
        }
    }
}
