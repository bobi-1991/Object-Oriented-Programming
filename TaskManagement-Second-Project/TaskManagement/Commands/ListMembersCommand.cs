using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core.Contracts;
using TaskManagement.Core;


namespace TaskManagement.Commands
{
    public class ListMembersCommand : BaseCommand
    {
        public ListMembersCommand(IRepository repository)
             : base(repository)
        {
        }

        public override string Execute()
        {
            
            return this.ShowMembers();
        }
        private string ShowMembers()
        {
            StringBuilder sb = new StringBuilder();

            var members = this.Repository.Members;

            if (members.Count == 0)
            {
                sb.AppendLine("Member list are empty");
            }
            else
            {
                int counter = 1;
                sb.AppendLine("--Members--");

                foreach (var member in members)
                {
                    sb.AppendLine($"{counter}. {member.Name}");
                    counter++;
                }
            }

            return sb.ToString();
        }
    }
}
