using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
   

    public class ListTasksByAssigneeCommand : BaseCommand
    {
        public const string DashesSeparator = "------------";

        public const int ExpectedNumberOfArguments = 1;
        public ListTasksByAssigneeCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string memberName = CommandParameters[0];

            return ListTasks(memberName);
        }
        private string ListTasks(string memberName)
        {
          var member = this.Repository.GetMember(memberName);

            var assigneableTasks = this.Repository.Teams.SelectMany(x => x.Boards)
                  .SelectMany(x => x.Tasks)
                  .Where(x => x is IBug || x is IStory).ToList();

            StringBuilder sb = new StringBuilder();

            if (assigneableTasks.Count == 0)
            {
                sb.AppendLine("Task list is empty");
            }
            else
            { 
                int counter = 1;

                foreach (var task in assigneableTasks.OrderBy(x => x.Title))
                { 
                    var assign = (IAssigneable)task;
                    if (assign.Assignee == member)
                    {
                        sb.AppendLine($"{counter}.{assign.ToString()}");
                        counter++;
                        sb.AppendLine($"{DashesSeparator}");
                    }  
                }
            }

            return sb.ToString();
        }
    }
}
