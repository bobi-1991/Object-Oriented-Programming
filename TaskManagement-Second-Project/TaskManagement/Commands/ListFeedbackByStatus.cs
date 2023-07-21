using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums;

namespace TaskManagement.Commands
{
    public class ListFeedbackByStatusCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 3;
        public const string DashesSeparator = "------------";


        public ListFeedbackByStatusCommand(IList<string> commandParameters, IRepository repository)
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
            FeedBackStatus finalStatus = ParseFeedbackStatusParameter(CommandParameters[2], "finalStatus");

            return this.ListAllTasks(teamName, boardName, finalStatus);
        }

        private string ListAllTasks(string teamName, string boardName, FeedBackStatus finalStatus)
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

            var listFeedback = board.Tasks.Where(f => f.TaskType == TaskType.Feedback);
            var listFeedbackStatus = listFeedback.Select(f => (Feedback)f).ToList();
            var result = listFeedbackStatus.Where(f => f.Status == finalStatus).ToList();

            if (result.Count == 0)
            {
                sb.AppendLine("Feedback list is empty");
            }
            else
            {
                sb.AppendLine($"--Feedback with Status: {finalStatus}--");

                foreach (var feedback in result)
                {
                    sb.AppendLine($"ID: {feedback.Id} Title: {feedback.Title} Rating: {feedback.Rating}");
                }
            }

            return sb.ToString();

            // Different print and sort by title

            //if (result.Count == 0)
            //{
            //    sb.AppendLine("Feedback list is empty");
            //}
            //else
            //{
            //    int counter = 1;

            //    foreach (var feedback in result.OrderBy(x=>x.Title))
            //    {
            //        sb.AppendLine($"{counter}.{feedback.ToString()}");
            //        sb.AppendLine(DashesSeparator);
            //    }
            //}

            //return sb.ToString();
        }

        private FeedBackStatus ParseFeedbackStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out FeedBackStatus result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be Active or Fixed.");
        }
    }
}
