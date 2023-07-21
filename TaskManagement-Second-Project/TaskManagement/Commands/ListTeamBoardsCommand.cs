using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Commands
{
    public class ListTeamBoardsCommand : BaseCommand
    {
        public ListTeamBoardsCommand(IRepository repository)
                    : base(repository)
        {
        }

        public override string Execute()
        {

            return this.ListTeamBoards();
        }
        private string ListTeamBoards()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Repository.Teams.Count == 0)
            {
                sb.AppendLine("Teams list are empty");
            }
            else if (this.Repository.Teams.All(x => x.Boards.Count == 0))
            {
                sb.AppendLine("Boards list are empty");
            }
            else
            {
                foreach (var team in this.Repository.Teams)
                {
                    int counter = 1;
                    sb.AppendLine($"--Team-- {team.Name}:");

                    if (team.Boards.Count == 0)
                    {
                        sb.AppendLine("Empty");
                    }
                    else
                    {
                        foreach (var board in team.Boards)
                        {
                            sb.AppendLine($"{counter}. {board.Name}");
                            counter++;
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
