using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Core.Contracts
{
    public interface IRepository
    {
        IList<IMember> Members { get; }
        IList<ITeam> Teams { get; }
        IList<IBoard> Boards { get; }
        IMember CreateMember(string name);
        ITeam CreateTeam(string name);
        void AddMemberToTeam(string memberName, string teamName);
        IStory CreateStory(string title, string description, Priority priority, StorySize size, string boardName, string teamName);
        IBug CreateBug(string title, string description, Priority priority, BugSeverity severity, string boardName, string teamName, List<string> steps);
        void CreateBoardInTeam(string boardName, string teamName);
        bool MemberExist(string username);
        bool TeamExist(string username);
        bool BoardExistInTeam(string teamName, string boardName);
        IStory GetStory(int id);
        IFeedback GetFeedback(int id);
        IBug GetBug(int id);
        ITeam GetTeam(string username);
        IFeedback CreateFeedback(string title, string description, int rating, string boardName, string teamName);
        IComment CreateComment(string content, IMember author);
        IMember GetMember(string author, string teamName, string boardName, int id);
        IAssigneable AssigneableMember(int id, string memberName);
        IMember GetMember(string memberName);
        ITaskBase GetTaskBase(int id);



    }
}
