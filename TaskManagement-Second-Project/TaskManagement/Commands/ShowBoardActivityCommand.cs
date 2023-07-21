using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ShowBoardActivityCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public const string DashesSeparator = "------------";

        public ShowBoardActivityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            string boardName = CommandParameters[0];

            return this.ShowBoardActivity(boardName);
        }
        private string ShowBoardActivity(string boardName)
        {
            StringBuilder sb = new StringBuilder();

            if (!this.Repository.Teams.SelectMany(x => x.Boards)
                .Any(x => x.Name == boardName))
            { 
                throw new InvalidUserInputException($"Board {boardName} is not exist.");
            }

            var board = this.Repository.Teams.SelectMany(x => x.Boards).FirstOrDefault(x=>x.Name == boardName);

            sb.AppendLine(board.ShowActivityHistory().ToString());
          
            return sb.ToString();
        }
    }
}
