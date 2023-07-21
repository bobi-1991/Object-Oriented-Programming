using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class UnassignTaskToMemberCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public UnassignTaskToMemberCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            int id = ParseIntParameter(CommandParameters[0], "id");
            string memberName = CommandParameters[1];

            return this.UnassignTaskToMember(id, memberName);
        }
        private string UnassignTaskToMember(int id, string memberName)
        {
            var member = this.Repository.GetMember(memberName);
            IAssigneable task = this.Repository.AssigneableMember(id, memberName);

            if (task.Assignee == null)
            {
                throw new ArgumentException($"This {task.GetType().Name} currently are unassigned.");
            }
            else if (task.Assignee.Name != memberName)
            {
                throw new ArgumentException($"This task is not assigned to {memberName}.");
            }
            else
            {
                task.Unassign();
            }

            return $"{memberName}'s task has been removed";
        }
    }
}
