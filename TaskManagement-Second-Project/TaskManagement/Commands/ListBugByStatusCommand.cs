using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Commands
{
    public class ListBugByStatusCommand : BaseCommand
    {
        public const string DashesSeparator = "------------";
        public const int ExpectedNumberOfArguments = 3;
        public ListBugByStatusCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string teamName = this.CommandParameters[0];
            string boardName = this.CommandParameters[1];
            BugStatus finalStatus = ParseBugStatusParameter(CommandParameters[2], "finalStatus");

            return this.ListAllTasks(teamName, boardName, finalStatus);
        }

        private string ListAllTasks(string teamName, string boardName, BugStatus finalStatus)
        {

            var team = base.Repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var teamExists = base.Repository.Teams.Any(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            if (!teamExists)
            {
                throw new ArgumentException($"Team with {teamName} does not exist.");
            }
            var boardExist = team.Boards.Any(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            if (!boardExist)
            {
                throw new ArgumentException($"Board with {boardName} does not exist.");
            }

            StringBuilder sb = new StringBuilder();

            var listBug = board.Tasks.Where(t => t.TaskType == TaskType.Bug);
            var listBugStatus = listBug.Select(b => (Bug)b).ToList();
            var result = listBugStatus.Where(b => b.Status == finalStatus).ToList();

            if (result.Count == 0)
            {
                sb.AppendLine("Bug list is empty");
            }
            else
            {
                sb.AppendLine($"--BUG with Status: {finalStatus}--");

                foreach (var bug in result)
                {
                    sb.AppendLine($"ID: {bug.Id} Title: {bug.Title} Priority: {bug.Priority}  Severity: {bug.Severity}");
                }
            }

            return sb.ToString();

            // Different print and sort by Title

            //StringBuilder sb = new StringBuilder();

            //var listBug = board.Tasks.Where(t => t.TaskType == TaskType.Bug);
            //var listBugStatus = listBug.Select(b => (Bug)b).ToList();
            //var result = listBugStatus.Where(b => b.Status == finalStatus).ToList();

            //if (result.Count == 0)
            //{
            //    sb.AppendLine("Bug list is empty");
            //}
            //else
            //{
            //    int counter = 1;

            //    foreach (var bug in result.OrderBy(x=>x.Title))
            //    {
            //        sb.AppendLine($"{counter}.{bug.ToString()}");
            //        sb.AppendLine(DashesSeparator);
            //    }
            //}

            //return sb.ToString();
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
