using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Commands
{
    public class HelpCommand : ICommand
    {
        public string Execute()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateMember", "Creates a New Member", "Example: CreateMember Pesho"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateTeam", "Creates a New Team", "Example: CreateTeam PeshoTeam"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListMembers", "List all Members", "Example: ListMembers"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListTeams", "List all Teams", "Example: ListTeams"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "AddMemberToTeam", "Adds a Member into a Team", "Example: AddMemberToTeam Pesho PeshoTeam"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListTeamMembers", "List all Members of a Team", "Example: ListTeamMembers PeshoTeam"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateBoardInTeam", "Creates a new board in team", "Example: CreateBoardInTeam ThisIsTestBoard GoshoTeam"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListTeamBoards", "List all team boards", "Example: ListTeamBoards"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateStory", "Creates a Story in a board", "Needs: Title/Description/priority/size/boardName/teamName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateFeedback", "Creates a Feedback in a board", "Needs: Title/Description/rating/boardName/teamName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "CreateBug", "Creates a bug in a board", "Needs: Title/Description/priority/severity/boardName/teamName/steps"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeBugPriority", "Changes Bug Priority", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeBugSeverity", "Changes Bug Severity", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeBugStatus", "Changes Bug Status", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeStoryPriority", "Changes Story Priority", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeStorySize", "Changes Story Size", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeStoryStatus", "Changes Story Status", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeFeedbackStatus", "Changes Feedback Status", "Needs: TaskID/advance or revert"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ChangeFeedbackRating", "Changes Feedback Rating", "Needs: TaskID/rating"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "AddComment", "Adds a comment to a task", "Needs: Author/TaskID/Content"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "AssignTaskToMember", "Assign task to a person.", "Needs: Id/memberName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "UassignTaskToMember", "Unassign task to a person.", "Needs: Id/memberName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListAllTasks", "List All Tasks in a Board", "Needs: TeamName/BoardName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListBugByStatus", "List All Bugs with Status", "Needs: TeamName/BoardName/Status"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListStoryByStatus", "List All Stories with Status", "Needs: TeamName/BoardName/Status"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListFeedbackByStatus", "List All Feedbacks with Status", "Needs: TeamName/BoardName/Status"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ListTasksByAssignee", "List All tasks with assignee", "Needs: memberName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ShowHistoryInTask", "Show all history in task", "Needs: TaskID"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ShowTeamActivity", "Show team activity", "Needs: teamName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ShowBoardActivity", "Show board activity", "Needs: boardName"));
            sb.AppendLine(String.Format("{0,-21} => {1,-32} => {2, 12}", "ShowMemberActivity", "Show member activity", "Needs: memberName"));

            return sb.ToString();
        }
    }
}
