using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Helper;

namespace TaskManagement.Tests.Models
{

    [TestClass]
    public class BoardTests
    {
        
        private  IList<string> activityHistory = new List<string>();

        [TestMethod]
        public void Should_CreateBoardIsOfCorrectType_When_ParametersAreValid()
        {
            var board = new Board("TestName");

            Assert.AreEqual("TestName", board.Name);
            Assert.IsInstanceOfType(board, typeof(Board), "Object is not of the expected type");
        }

        [TestMethod]
        [DataRow(Board.BoardNameMinLength - 1)]
        [DataRow(Board.BoardNameMaxLength + 1)]
        public void Constructor_Should_Throw_When_BoardNameIsOutOfBonds(int value)
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Board(new string('x', value)));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_BoardNameIsNull()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Board(null));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_BoardNameIsWhiteSpace()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Board(new string(' ', 10)));
        }

        [TestMethod]
        public void Tasks_Should_ReturnCopyOfTheCollection()
        {
            var board = new Board("TestName");

            ITaskBase bug = TestHelpers.GetTestBug();
            board.Tasks.Add(bug);

            Assert.AreEqual(0, board.Tasks.Count);
        }

        [TestMethod]
        public void ActivityHistory_Should_ReturnCopyOfTheCollection()
        {
            string history = "Test History";
            string boardName = "TestName";
            var board = new Board(boardName);

            board.ActivityHistory.Add(history);

            Assert.AreEqual(0, activityHistory.Count);
        }

        [TestMethod]
        public void Should_AddBugInBoardTasks()
        {
            string boardName = "TestName";
            var board = new Board(boardName);
            ITaskBase bug = TestHelpers.GetTestBug();

            board.AddBug(bug);

            Assert.AreEqual(1, board.Tasks.Count());
        }

        [TestMethod]
        public void Should_AddStoryInBoardTasks()
        {
            string boardName = "TestName";
            var board = new Board(boardName);
            ITaskBase story = TestHelpers.GetTestStory();

            board.AddStory(story);

            Assert.AreEqual(1, board.Tasks.Count());
        }

        [TestMethod]
        public void Should_AddFeedbackInBoardTasks()
        {
            string boardName = "TestName";
            var board = new Board(boardName);
            ITaskBase feedback= TestHelpers.GetTestFeedback();

            board.AddFeedback(feedback);

            Assert.AreEqual(1, board.Tasks.Count());
        }
    }
}
