using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Team : ITeam
    {
        public const int TeamNameMinLength = 5;
        public const int TeamNameMaxLength = 15;
        public const string TeamNameErrorMessage = "Team name cannot be less than {0} and more than {1} symbols";
        public const string NullOrWhiteSpaceErrorMessage = "Incorrect data entered (The imput cannot be empty)! Please try again";

        private string name;
        private readonly List<IMember> members = new List<IMember>();
        private readonly List<IBoard> boards = new List<IBoard>();

        public Team(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                Validator.ValidateIsNotNullOrWhiteSpace(value, NullOrWhiteSpaceErrorMessage);
                Validator.ValidateIntRange(value.Length, TeamNameMinLength, TeamNameMaxLength, TeamNameErrorMessage);
                this.name = value;
            }
        }
        public List<IMember> Members
        {
             get { return this.members; }
          //  get => new List<IMember>(this.members);
        }
        public List<IBoard> Boards
        {
           // get { return this.boards; }
            get => new List<IBoard>(this.boards);
        }

        public void AddBoard(IBoard board)
        {
            this.boards.Add(board);
        }
        //public void RemoveBoard(IBoard board)
        //{
        //    this.boards.Remove(board);
        //}
 
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team name: {this.Name}");

            return sb.ToString();
        }

        public void AddMember(IMember member)
        {
           this.members.Add(member);
        }
    }
}
