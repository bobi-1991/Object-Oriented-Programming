using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManagement.Commands
{
    public class CreateBugCommand:BaseCommand
    {
        public const int ExpectedNumberOfArguments = 4;

        public CreateBugCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }
         //   string title, string description, List< string > steps, Priority priority, BugSeverity severity

            string title = CommandParameters[0];
            string description = CommandParameters[1];
            Priority priority = ParsePriorityParameter(CommandParameters[2],"priority");
            BugSeverity severity = this.ParseBugSeverityParameter(CommandParameters[3], "severity");
            string boardName = CommandParameters[4];
            string teamName = CommandParameters[5];

            var steps = new List<string>();

            for (int i = 6; i < CommandParameters.Count; i++)
            {
                steps.Add(CommandParameters[i]);
            }
           

            return this.CreateBug(title, description, priority, severity, boardName, teamName, steps);

        }

        private string CreateBug(string title, string description, Priority priority, BugSeverity severity, string boardName, string teamName,List<string> steps)
        {
            if (!base.Repository.TeamExist(teamName))
            {
                throw new InvalidUserInputException($"Team name {teamName} is not exist");
            }
            if (!base.Repository.BoardExistInTeam(teamName,boardName))
            {
                throw new InvalidUserInputException($"Board name {boardName} is not exist");
            }

            base.Repository.CreateBug(title, description, priority, severity, boardName, teamName, steps);

            var team = base.Repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            var currentBug = board.Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            int bugID = currentBug.Id;
            return $"Bug with title {title} and ID: {bugID} was created";
        }
        private BugSeverity ParseBugSeverityParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out BugSeverity result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be either Critical, Major or Minor");
        }
        private BugStatus ParseBugStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out BugStatus result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be Active or Fixed.");
        }
    }
}
