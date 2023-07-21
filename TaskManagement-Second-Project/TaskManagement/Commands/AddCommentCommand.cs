using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 3;

        public AddCommentCommand(List<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string author = this.CommandParameters[0];
            int id = this.ParseIntParameter(this.CommandParameters[1], "id");
            string content = "";
            for (int i = 2; i < CommandParameters.Count; i++)
            {
                content+= CommandParameters[i] + " ";
            }

            return this.AddComment(author, id,content);
        }

        private string AddComment(string author, int id,string content)
        {
            var task = this.Repository.GetTaskBase(id);
            var member = this.Repository.GetMember(author);

            if (task == null)
            {
                throw new InvalidUserInputException($"Task with ID {id} is not exist.");
            }
 
            var comment = this.Repository.CreateComment(content, member);
            member.AddComment(comment, task);

            return $"{author} added comment successfully!";
        }
    }
}
