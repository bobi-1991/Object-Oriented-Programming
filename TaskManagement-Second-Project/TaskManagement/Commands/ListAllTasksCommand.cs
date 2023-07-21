using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace TaskManagement.Commands
{
    public class ListAllTasksCommand : BaseCommand
    {
        public const string DashesSeparator = "------------";
        public const int ExpectedNumberOfArguments = 2;

        public ListAllTasksCommand(IList<string> commandParameters, IRepository repository)
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

            return this.ListAllTasks(teamName, boardName);
        }

        private string ListAllTasks(string teamName, string boardName)
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

            var tasks = board.Tasks.OrderBy(t => t.Title).ToList();

            if (tasks.Count == 0)
            {
                sb.AppendLine("Tasks list is empty");
            }
            else
            {
                sb.AppendLine("--TASKS--");

                foreach (var task in tasks)
                {
                    sb.AppendLine($"ID: {task.Id} Title: {task.Title} Description: {task.Description}");
                }
            }

            return sb.ToString();



            // Filter by Title / Sort by Title version // All tasks without sort teamName and boardName 

            //StringBuilder sb = new StringBuilder();
            //var tasks = board.Tasks.Where(x => x.Title == "title ot imput")
            //    .OrderBy(t => t.Title).ToList();

            //if (tasks.Count == 0)
            //{
            //    sb.AppendLine("Task list is empty");
            //}
            //else
            //{
            //    int counter = 1;

            //    foreach (var task in tasks)
            //    {
            //        sb.AppendLine($"{counter}.{task.ToString()}");
            //        counter++;
            //        sb.AppendLine($"{DashesSeparator}");

            //    }
            //}

            //return sb.ToString();
        }
    }
}
