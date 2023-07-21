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
    public class ListStoryByStatusCommand : BaseCommand
    {
        public const string DashesSeparator = "------------";
        public const int ExpectedNumberOfArguments = 3;
        public ListStoryByStatusCommand(IList<string> commandParameters, IRepository repository)
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
            StoryStatus finalStatus = ParseStoryStatusParameter(CommandParameters[2], "finalStatus");

            return this.ListAllTasks(teamName, boardName, finalStatus);
        }

        private string ListAllTasks(string teamName, string boardName, StoryStatus finalStatus)
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

            var listStory = board.Tasks.Where(t => t.TaskType == TaskType.Story);
            var listStoryStatus = listStory.Select(s => (Story)s).ToList();
            var result = listStoryStatus.Where(s => s.Status == finalStatus).ToList();

            if (result.Count == 0)
            {
                sb.AppendLine("Story list is empty");
            }
            else
            {
                sb.AppendLine($"--Story with Status: {finalStatus}--");

                foreach (var story in result)
                {
                    sb.AppendLine($"ID: {story.Id} Title: {story.Title} Priority: {story.Priority}  Size: {story.Size}");
                }
            }

            return sb.ToString();

            //Different variant to print info and add Sort by Title

            //if (result.Count == 0)
            //{
            //    sb.AppendLine("Story list is empty");
            //}
            //else
            //{
            //    int counter = 1;

            //    foreach (var story in result.OrderBy(x=>x.Title))
            //    {
            //        sb.AppendLine($"{counter}.{story.ToString()}");
            //        sb.AppendLine(DashesSeparator);
            //    }
            //}

            //return sb.ToString();
        }

        private StoryStatus ParseStoryStatusParameter(string value, string parameterName)
        {
            if (Enum.TryParse(value, true, out StoryStatus result))
            {
                return result;
            }
            throw new InvalidUserInputException($"Invalid value for {parameterName}. Should be Active or Fixed.");
        }
    }
}
