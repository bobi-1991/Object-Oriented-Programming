using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class TeamTests
    {

        [TestMethod]
        public void Team_ShouldImplement_ITeamInterface()
        {
            var type = typeof(Team);
            var isAssignable = typeof(ITeam).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Team does not implement ITeam interface");
        }

        [TestMethod]
        public void Should_CreateTeam_When_ParametersAreValid()
        {
            string teamName = "TestName";

            var team = new Team(teamName);

            Assert.AreEqual(teamName, team.Name);
            Assert.IsInstanceOfType(team, typeof(Team), "Object is not of the expected type");
        }

        [TestMethod]
        [DataRow(Team.TeamNameMinLength - 1)]
        [DataRow(Team.TeamNameMaxLength + 1)]
        public void Constructor_Should_Throw_When_TeamNameIsOutOfBonds(int value)
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Team(new string('x', value)));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_TeamNameIsNull()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Team(null));
        }

        [TestMethod]
        public void Constructor_Should_Throw_When_TeamNameIsWhiteSpace()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Team(new string(' ', 10)));
        }

        [TestMethod]
        public void AddMember_Should_AddMemberInTeam()
        {
            string memberName = "TestMember";
            string teamName = "TestTeam";

            var member = new Member(memberName);
            var team = new Team(teamName);

            team.AddMember(member);

            var list = team.Members.FirstOrDefault();
            Assert.AreEqual(member, list);
        }

        [TestMethod]
        public void AddBoard_Should_AddBoardInTeam()
        {
            string boardName = "TestBoard";
            string teamName = "TestTeam";

            var board = new Board(boardName);
            var team = new Team(teamName);

            team.AddBoard(board);

            var list = team.Boards.FirstOrDefault();
            Assert.AreEqual(board, list);
        }
    }
}
