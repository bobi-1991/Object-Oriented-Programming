using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class AssignTaskToMemberCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public AssignTaskToMemberCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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

            return this.AssignTaskToMember(id, memberName);
        }
        private string AssignTaskToMember(int id, string memberName)
        {
            var member = this.Repository.GetMember(memberName);
            IAssigneable task = this.Repository.AssigneableMember(id, memberName);

            if (task.Assignee != null)
            {
                throw new ArgumentException($"This {task.GetType().Name} is already assigned.");
            }
            task.AddAssignee(member);

            return $"{task.GetType().Name} with id {id} are assign to {memberName}.";
        }
    }
}
