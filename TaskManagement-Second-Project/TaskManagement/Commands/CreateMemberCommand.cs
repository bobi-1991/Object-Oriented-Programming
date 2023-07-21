using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class CreateMemberCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public CreateMemberCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }
        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string name = this.CommandParameters[0];

            return this.CreateMember(name);
        }
        private string CreateMember(string memberName)
        {
            if (this.Repository.MemberExist(memberName))
            {
                string errorMessage = $"Member with name {memberName} already exist.";
                throw new ArgumentException(errorMessage);
            }

            base.Repository.CreateMember(memberName);

            return $"Member with name: {memberName} was created";
        }
    }
}
