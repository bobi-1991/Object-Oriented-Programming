using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core;
using TaskManagement.Exceptions;

namespace TaskManagement.Tests.Commands.Core
{
    [TestClass]
    public class CommandFactoryTests
    {
        private Repository repository;
        private CommandFactory commandFactory;

        [TestInitialize]

        public void Initialize()
        {
            repository = new Repository();
            commandFactory = new CommandFactory(repository);
        }
        [TestMethod]
        public void Create_CreateMember_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateMember");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateMemberCommand));
        }

        [TestMethod]
        public void Create_CreateTeam_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateTeam");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateTeamCommand));
        }

        [TestMethod]
        public void Create_ListMembers_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListMembers");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListMembersCommand));
        }

        [TestMethod]
        public void Create_ListTeams_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListTeams");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListTeamsCommand));
        }

        [TestMethod]
        public void Create_AddMemberToTeam_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("AddMemberToTeam");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(AddMemberToTeamCommand));
        }

        [TestMethod]
        public void Create_ListTeamMembers_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListTeamMembers");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListTeamMembers));
        }

        [TestMethod]
        public void Create_CreateBoardInTeam_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateBoardInTeam");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateBoardInTeamCommand));
        }


        [TestMethod]
        public void Create_ListTeamBoards_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListTeamBoards");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListTeamBoardsCommand));
        }

        [TestMethod]
        public void Create_CreateStory_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateStory");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateStoryCommand));
        }

        [TestMethod]
        public void Create_CreateBug_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateBug");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateBugCommand));
        }

        [TestMethod]
        public void Create_CreateFeedback_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("CreateFeedback");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(CreateFeedbackCommand));
        }

        [TestMethod]
        public void Create_ChangeBugPriority_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeBugPriority");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeBugPriorityCommand));
        }

        [TestMethod]
        public void Create_ChangeBugSeverity_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeBugSeverity");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeBugSeverityCommand));
        }

        [TestMethod]
        public void Create_ChangeBugStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeBugStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeBugStatusCommand));
        }

        [TestMethod]
        public void Create_ChangeStoryPriority_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeStoryPriority");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeStoryPriorityCommand));
        }

        [TestMethod]
        public void Create_ChangeStorySize_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeStorySize");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeStorySizeCommand));
        }

        [TestMethod]
        public void Create_ChangeStoryStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeStoryStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeStoryStatusCommand));
        }

        [TestMethod]
        public void Create_ChangeFeedbackStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeFeedbackStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeFeedbackStatusCommand));
        }

        [TestMethod]
        public void Create_ChangeFeedbackRating_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ChangeFeedbackRating");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ChangeFeedbackRatingCommand));
        }

        [TestMethod]
        public void Create_AddComment_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("AddComment");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(AddCommentCommand));
        }

        [TestMethod]
        public void Create_AssignTaskToMember_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("AssignTaskToMember");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(AssignTaskToMemberCommand));
        }

        [TestMethod]
        public void Create_UnassignTaskToMember_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("UnassignTaskToMember");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(UnassignTaskToMemberCommand));
        }

        [TestMethod]
        public void Create_ListAllTasks_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListAllTasks");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListAllTasksCommand));
        }

        [TestMethod]
        public void Create_ListBugByStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListBugByStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListBugByStatusCommand));
        }

        [TestMethod]
        public void Create_ListStoryByStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListStoryByStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListStoryByStatusCommand));
        }

        [TestMethod]
        public void Create_ListFeedbackByStatus_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListFeedbackByStatus");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListFeedbackByStatusCommand));
        }

        [TestMethod]
        public void Create_ListTasksByAssignee_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ListTasksByAssignee");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ListTasksByAssigneeCommand));
        }

        [TestMethod]
        public void Create_ShowHistoryInTask_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ShowHistoryInTask");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ShowHistoryInTaskCommand));
        }

        [TestMethod]
        public void Create_ShowTeamActivity_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ShowTeamActivity");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ShowTeamActivityCommand));
        }

        [TestMethod]
        public void Create_ShowBoardActivity_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ShowBoardActivity");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ShowBoardActivityCommand));
        }

        [TestMethod]
        public void Create_ShowMemberActivity_ReturnResult()
        {
            //Act
            var result = commandFactory.Create("ShowMemberActivity");

            //Asert
            Assert.IsInstanceOfType(result, typeof(ICommand));
            Assert.IsInstanceOfType(result, typeof(ShowMemberActivityCommand));
        }

        [TestMethod]
        public void Create_InvalidInput_ShouldThrow()
        {
            //Act && Assert
            var ex = Assert.ThrowsException<InvalidUserInputException>(
                () => commandFactory.Create("InvalidInput"));

            Assert.AreEqual("Command with this parameter does not exist. Please try with valid parameter or type Help for guidance.", ex.Message);
        }

    }
}
