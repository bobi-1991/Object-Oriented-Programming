using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models;
using TaskManagement.Tests.Helper;
using TaskManagement.Exceptions;
using System.Diagnostics.Metrics;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.Commands.Core
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository repository;
        
        [TestInitialize]
        public void Initialize()
        {
            repository = TestHelpers.GetTestRepository();
        }

        [TestMethod]
        public void CreateMember_ShouldCreateMember()
        {
            string memberName = "TestName";

            var result = this.repository.CreateMember(memberName);

            Assert.IsNotNull(result);
            Assert.AreEqual(memberName, result.Name);
            Assert.AreEqual(1, this.repository.Members.Count());
            var memberList = this.repository.Members.FirstOrDefault();
            Assert.AreEqual(result.Name, memberList.Name);
        }

        [TestMethod]
        public void CreateTeam_ShouldCreateTeam()
        {
            string teamName = "TestTeam";

            var result = this.repository.CreateTeam(teamName);

            Assert.IsNotNull(result);
            Assert.AreEqual(teamName, result.Name);
            Assert.AreEqual(1, this.repository.Teams.Count());
            var teamList = this.repository.Teams.FirstOrDefault();
            Assert.AreEqual(result.Name, teamList.Name);
        }

        [TestMethod]
        public void AddMemberToTeam_ShouldAddMember()
        {
            string memberName = "TestMember";
            string teamName = "TestTeam";
            this.repository.CreateMember(memberName);
            this.repository.CreateTeam(teamName);

            this.repository.AddMemberToTeam(memberName, teamName);
            var team = this.repository.Teams.First(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual(1, team.Members.Count());
            var membersList = team.Members.FirstOrDefault();
            Assert.AreEqual(memberName, membersList.Name);
        }

        [TestMethod]
        public void AddMemberToTeam_ShouldNotAddMember_ShouldThrow()
        {
            string memberName = "TestMember";
            string memberName2 = "TestMember";
            string teamName = "TestTeam";
            this.repository.CreateMember(memberName);
            this.repository.CreateTeam(teamName);
            this.repository.AddMemberToTeam(memberName, teamName);

            var ex = Assert.ThrowsException<ArgumentException>(() => this.repository.AddMemberToTeam(memberName2, teamName));
            Assert.AreEqual($"{memberName} already exists in team: {teamName}", ex.Message);
        }

        [TestMethod]
        public void CreateStory_ShouldCreateStory()
        {
            var title = "StoryTitleTitle";
            var description = "StoryDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateStory(title, description, Priority.Low, StorySize.Medium, boardName, teamName);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            Assert.AreEqual(1, board.Tasks.Count());
            var story = board.Tasks.FirstOrDefault();
            Assert.AreEqual(title, story.Title);
        }

        [TestMethod]
        public void CreateStory_ShouldNotCreateStory_ShouldThrow()
        {
            var title = "StoryTitleTitle";
            var title2 = "StoryTitleTitle";
            var description = "StoryDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateStory(title, description, Priority.Low, StorySize.Medium, boardName, teamName);

            var ex = Assert.ThrowsException<ArgumentException>(() => this.repository.CreateStory(title2, description, Priority.Low, StorySize.Medium, boardName, teamName));
            Assert.AreEqual($"Task/Story: {title} already exists in board: {boardName}", ex.Message);
        }

        [TestMethod]
        public void CreateFeedback_ShouldCreateFeedback()
        {
            var title = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(title, description, 8, boardName, teamName);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            Assert.AreEqual(1, board.Tasks.Count());
            var feedback = board.Tasks.FirstOrDefault();
            Assert.AreEqual(title, feedback.Title);
        }

        [TestMethod]
        public void CreateFeedack_ShouldNotCreateFeedback_ShouldThrow()
        {
            var title = "FeedbackTitleTitle";
            var title2 = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(title, description, 8, boardName, teamName);

            var ex = Assert.ThrowsException<ArgumentException>(() => this.repository.CreateFeedback(title2, description, 8, boardName, teamName));
            Assert.AreEqual($"Task/Feedback: {title} already exists in board: {boardName}", ex.Message);
        }

        [TestMethod]
        public void CreateBug_ShouldCreateBug()
        {
            var title = "BugTitleTitle";
            var description = "BugDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateBug(title, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));

            Assert.AreEqual(1, board.Tasks.Count());
            var bug = board.Tasks.FirstOrDefault();
            Assert.AreEqual(title, bug.Title);
        }

        [TestMethod]
        public void CreateBug_ShouldNotCreateBug_ShouldThrow()
        {
            var title = "BugTitleTitle";
            var title2 = "BugTitleTitle";
            var description = "BugDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateBug(title, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var ex = Assert.ThrowsException<ArgumentException>(() => this.repository.CreateBug(title2, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps));
            Assert.AreEqual($"Task/Bug: {title} already exists in board: {boardName}", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetStory_ShouldReturnStoryWithID()
        {
            var title = "StoryTitleTitle";
            var description = "StoryDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateStory(title, description, Priority.Low, StorySize.Medium, boardName, teamName);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var result = this.repository.GetStory(1);

            Assert.AreEqual(1, board.Tasks.Count());
            var story = board.Tasks.FirstOrDefault();
            Assert.AreEqual(story.Title, result.Title);
        }

        [TestMethod] //Run Separately
        public void GetStory_WhenInvalidID_ShouldThrow()
        {
            var title = "StoryTitleTitle";
            var description = "StoryDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateStory(title, description, Priority.Low, StorySize.Medium, boardName, teamName);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetStory(2));
            Assert.AreEqual($"Id 2 is not exist", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetStory_WhenInvalidIDAndIsDifferntTask_ShouldThrow()
        {
            var title = "StoryTitleTitle";
            var titleFeedback = "FeedbackTitleTitle";
            var description = "StoryDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateStory(title, description, Priority.Low, StorySize.Medium, boardName, teamName);
            this.repository.CreateFeedback(titleFeedback, description, 8, boardName, teamName);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetStory(2));
            Assert.AreEqual($"This task is not a story. Please put correct ID", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetFeedack_ShouldReturnFeedbackWithID()
        {
            var title = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(title, description, 8, boardName, teamName);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var result = this.repository.GetFeedback(1);

            Assert.AreEqual(1, board.Tasks.Count());
            var feedback = board.Tasks.FirstOrDefault();
            Assert.AreEqual(feedback.Title, result.Title);
        }

        [TestMethod] //Run Separately
        public void GetFeedback_WhenInvalidID_ShouldThrow()
        {
            var title = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(title, description, 8, boardName, teamName);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetFeedback(2));
            Assert.AreEqual($"Id 2 is not exist", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetFeedback_WhenInvalidIDAndIsDifferntTask_ShouldThrow()
        {
            var titleStory = "StoryTitleTitle";
            var titleFeedback = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(titleFeedback, description, 8, boardName, teamName);
            this.repository.CreateStory(titleStory , description, Priority.Low, StorySize.Medium, boardName, teamName);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetFeedback(2));
            Assert.AreEqual($"This task is not a feedback. Please put correct ID", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetBug_ShouldReturnBugWithID()
        {
            var title = "BugTitleTitle";
            var description = "BugDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateBug(title, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var team = this.repository.Teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            var board = team.Boards.First(b => b.Name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            var result = this.repository.GetBug(1);

            Assert.AreEqual(1, board.Tasks.Count());
            var bug = board.Tasks.FirstOrDefault();
            Assert.AreEqual(bug.Title, result.Title);
        }

        [TestMethod] //Run Separately
        public void GetBug_WhenInvalidID_ShouldThrow()
        {
            var title = "BugTitleTitle";
            var description = "BugDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateBug(title, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetBug(2));
            Assert.AreEqual($"Id 2 is not exist", ex.Message);
        }

        [TestMethod] //Run Separately
        public void GetBug_WhenInvalidIDAndIsDifferntTask_ShouldThrow()
        {
            var titleBug = "BugTitleTitle";
            var titleFeedback = "FeedbackTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateFeedback(titleFeedback, description, 8, boardName, teamName);
            this.repository.CreateBug(titleBug, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetBug(1));
            Assert.AreEqual($"This task is not a bug. Please put correct ID", ex.Message);
        }

        [TestMethod]
        public void GetMember_ShouldReturnMember()
        {
            var memberName = "TestMember";
            var teamName = "TestTeam";
            this.repository.CreateMember(memberName);
            this.repository.CreateTeam(teamName);
            this.repository.AddMemberToTeam(memberName, teamName);
            var result = this.repository.GetMember(memberName);

            Assert.AreEqual(memberName, result.Name);
        }

        [TestMethod]
        public void GetMember_ShouldThrow()
        {
            var memberName = "TestMember";
            var memberName2 = "TestMember2";
            var teamName = "TestTeam";
            this.repository.CreateMember(memberName);
            this.repository.CreateTeam(teamName);
            this.repository.AddMemberToTeam(memberName, teamName);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => this.repository.GetMember(memberName2));
            Assert.AreEqual($"Member with name {memberName2} is not exist", ex.Message);
        }

        [TestMethod]
        public void GetTaskBase_ShouldReturn()
        {
            var titleBug = "BugTitleTitle";
            var description = "FeedbackDescriptionDescription";
            var teamName = "TestTeam";
            var boardName = "TestBoard";
            List<string> bugSteps = new List<string>() { "aide, aide, aide" };
            this.repository.CreateTeam(teamName);
            this.repository.CreateBoardInTeam(boardName, teamName);
            this.repository.CreateBug(titleBug, description, Priority.Medium, BugSeverity.Critical, boardName, teamName, bugSteps);

            var taskBase = this.repository.GetTaskBase(1);
            Assert.AreEqual(titleBug, taskBase.Title);
        }

        [TestMethod]
        public void BoardExistInTeam_ShouldReturnTrue()
        {
            var team = "TeamName";
            var board = "BoardName";
            this.repository.CreateTeam(team);
            this.repository.CreateBoardInTeam(board, team);

            var isExist = this.repository.BoardExistInTeam(team, board);
            Assert.IsTrue(isExist);
        }

        [TestMethod]
        public void BoardExistInTeam_ShouldReturnFalse()
        {
            var team = "TeamName";
            var board = "BoardName";
            var board2 = "BoardName2";
            this.repository.CreateTeam(team);
            this.repository.CreateBoardInTeam(board, team);

            var isExist = this.repository.BoardExistInTeam(team, board2);
            Assert.IsFalse(isExist);
        }

        [TestMethod]
        public void CreateComment_ShouldCreate()
        {
            var content = new string('x', 3);
            var member = this.repository.CreateMember("memberName");
            
            var comment = this.repository.CreateComment(content, member);

            Assert.AreEqual(content, comment.Content);
            Assert.AreEqual(member.Name, comment.Author);
            Assert.IsInstanceOfType(comment, typeof(Comment), "Object is not of the expected type");
        }

        [TestMethod]
        public void MemberExist_ReturnsTrue_When_MemberExists()
        {
            var memberName = "TestName";

            this.repository.CreateMember(memberName); 
            var result = this.repository.MemberExist(memberName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MemberExist_ReturnsFalse_When_MemberDoesNotExists()
        {
            var memberName = "TestName";

            var result = this.repository.MemberExist(memberName);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TeamExist_ReturnsTrue_When_TeamExists()
        {
            var teamName = "TestTeam";

            this.repository.CreateTeam(teamName);
            var result = this.repository.TeamExist(teamName);

            Assert.IsTrue(result); 
        }
  
        [TestMethod]
        public void TeamExist_ReturnsFalse_When_TeamDoesNotExist()
        {
            var teamName = "TestTeam";

            var result = this.repository.TeamExist(teamName);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Test_GetTeam_ReturnsTeamWhenTeamExists()
        {
            var teamName = "TeamTest";

            var team = new Team(teamName); 
            this.repository.CreateTeam(teamName); 

            var result = this.repository.GetTeam(teamName);

            Assert.IsNotNull(result);
            Assert.AreEqual(teamName, result.Name, ignoreCase: true); 
        }

        [TestMethod]
        public void Test_GetTeam_ThrowsArgumentExceptionWhenTeamDoesNotExist()
        {
            var teamName = "NonExistingTeam";

            //var result = this.repository.GetTeam(teamName);

            Assert.ThrowsException<ArgumentException>(()=> this.repository.GetTeam("someTeam"));
        }

    }
}
