using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;

namespace TaskManagement.Commands
{
    public class ShowMemberActivityCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public const string DashesSeparator = "------------";

        public ShowMemberActivityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string memberName = this.CommandParameters[0];

            return this.ShowMembersActivity(memberName);
        }
        private string ShowMembersActivity(string memberName)
        {
            var member = this.Repository.Members.FirstOrDefault(x => x.Name == memberName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(member.ShowActivityHistory().ToString());

            return sb.ToString();
        }
    }
}
