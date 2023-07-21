using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Core
{
    public class Repository : IRepository
    {
        private readonly IList<IMember> members = new List<IMember>();
        private readonly IList<ITeam> teams = new List<ITeam>();
        private readonly IList<IBoard> boards = new List<IBoard>();

        public IList<IMember> Members
        {
            get
            {
                var usersCopy = new List<IMember>(this.members);
                return usersCopy;
            }
        }
        public IList<ITeam> Teams
        {
            get
            {
                var usersCopy = new List<ITeam>(this.teams);
                return usersCopy;
            }
        }
        public IList<IBoard> Boards
        {
            get
            {
                var usersCopy = new List<IBoard>(this.boards);
                return usersCopy;
            }
        }

        public void AddMemberToTeam(string memberName, string teamName)
        {
            var team = this.teams.First(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var member = this.members.First(x => x.Name.Equals(memberName, StringComparison.InvariantCultureIgnoreCase));

            var isExist = team.Members.Any(m => m.Name.Equals(member.Name, StringComparison.InvariantCultureIgnoreCase));

            if (isExist)
            {
                    throw new ArgumentException($"{member.Name} already exists in team: {team.Name}");
            }

            team.AddMember(member);
        }

        public IMember CreateMember(string name)
        {
            var member = new Member(name);
            members.Add(member);
            return member;
        }

        public IStory CreateStory(string title, string description, Priority priority, StorySize size, string boardName, string teamName)
        {
            var team = this.teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var isExists = board.Tasks.Any(s => s.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            if (isExists)
            {
                throw new ArgumentException($"Task/Story: {title} already exists in board: {board.Name}");
            }
            var story = new Story(title, description, priority, size);
            board.AddStory(story);
            return story;
        }

        public ITeam CreateTeam(string name)
        {
            var team = new Team(name);
            teams.Add(team);
            return team;
        }

        public bool MemberExist(string memberName)
        {
            return this.members.Any(m => m.Name.Equals(memberName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool TeamExist(string teamName)
        {
            return this.teams.Any(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
        }
 
        public ITeam GetTeam(string teamName)
        {
            if (!TeamExist(teamName))
            {
                throw new ArgumentException($"Team with name: {teamName} does not exist");
            }
            return this.teams.First(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void CreateBoardInTeam(string boardName, string teamName)
        {
            var team = this.teams.First(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var isExist = team.Boards.Any(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            if (isExist)
            {
                throw new ArgumentException($"{boardName} already exists in team: {team.Name}");
            }

            var board = new Board(boardName);
            team.AddBoard(board);
        }

        public IFeedback CreateFeedback(string title, string description, int rating, string boardName, string teamName)
        {
            var team = this.teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var isExists = board.Tasks.Any(f => f.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            if (isExists)
            {
                throw new ArgumentException($"Task/Feedback: {title} already exists in board: {board.Name}");
            }
            var feedback = new Feedback(title, description, rating);
            board.AddFeedback(feedback);
            return feedback;
        }

        public IBug CreateBug(string title, string description, Priority priority, BugSeverity severity, string boardName, string teamName, List<string> steps)
        {
            var team = this.teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var isExists = board.Tasks.Any(b => b.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));

            if (isExists)
            {
                throw new ArgumentException($"Task/Bug: {title} already exists in board: {board.Name}");
            }
            var bug = new Bug(title, description, priority, severity, steps);
            board.AddBug(bug);
            return bug;

        }
        public IStory GetStory(int id)
        {
            foreach (var story in this.teams.SelectMany(x => x.Boards)
        .SelectMany(x => x.Tasks)
        .Where(x => x.Id == id))

                if (story is IStory)
                {
                    return (IStory)story;
                }
                else if (story is IBug || story is IFeedback)
                {
                    throw new InvalidUserInputException($"This task is not a story. Please put correct ID");
                }

            throw new InvalidUserInputException($"Id {id} is not exist");
        }

        public IFeedback GetFeedback(int id)
        {
            foreach (var feedback in this.teams.SelectMany(x => x.Boards)
 .SelectMany(x => x.Tasks)
 .Where(x => x.Id == id))

                if (feedback is IFeedback)
                {
                    return (IFeedback)feedback;
                }
                else if (feedback is IStory || feedback is IBug)
                {
                    throw new InvalidUserInputException($"This task is not a feedback. Please put correct ID");
                }

            throw new InvalidUserInputException($"Id {id} is not exist");
        }

        public IBug GetBug(int id)
        {
            foreach (var bug in this.teams.SelectMany(x => x.Boards)
.SelectMany(x => x.Tasks)
.Where(x => x.Id == id))

                if (bug is IBug)
                {
                    return (IBug)bug;
                }
                else if (bug is IFeedback || bug is IStory)
                {
                    throw new InvalidUserInputException($"This task is not a bug. Please put correct ID");
                }

            throw new InvalidUserInputException($"Id {id} is not exist");
        }

        public IComment CreateComment(string content, IMember author)
        {
            return new Comment(content, author.Name);
           
        }

        public IMember GetMember(string author, string teamName, string boardName, int id)
        {
            var team = this.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var task = board.Tasks.FirstOrDefault(t => t.Id == id);
            var member = team.Members.FirstOrDefault(m => m.Name.Equals(author, StringComparison.InvariantCultureIgnoreCase));
            return (member);
        }

        public IAssigneable AssigneableMember(int id, string memberName)
        {
            foreach (var item in this.teams.SelectMany(x => x.Boards)
                .SelectMany(x => x.Tasks)
                .Where(x => x.Id == id))

                if (item is IBug || item is IStory)
                {
                    return (IAssigneable)item;
                }
                else if (item is IFeedback)
                {
                    throw new InvalidUserInputException("This task is Feedback! It cannot be assigned");
                }

            throw new InvalidUserInputException($"Id {id} is not exist");
        }
        public IMember GetMember(string memberName)
        {
            foreach (var member in this.Teams.SelectMany(x => x.Members)
                .Where(m => m.Name.Equals(memberName)))
                return member;

            throw new InvalidUserInputException($"Member with name {memberName} is not exist");
        }

        public ITaskBase GetTaskBase(int id)
        {
           return this.teams.SelectMany(x => x.Boards)
                .SelectMany(x => x.Tasks)
                .FirstOrDefault(x => x.Id == id);
        }

        public bool BoardExistInTeam(string teamName, string boardName)
        {
            var team = this.GetTeam(teamName);
            return team.Boards.Any(x => x.Name == boardName);
        }
    }
}
