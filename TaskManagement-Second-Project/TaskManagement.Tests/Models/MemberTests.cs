using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Helper;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class MemberTests
    {
        [TestMethod]
        public void Member_ShouldImplement_IMemberInterface()
        {
            var type = typeof(Member);
            var isAssignable = typeof(IMember).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Member does not implement IMember interface");
        }

        [TestMethod]
        public void Member_Create_WhenAllParametersAreValid()
        {
            var name = "TestMember";
            var member = new Member(name);

            Assert.AreEqual(name, member.Name);
            Assert.IsInstanceOfType(member, typeof(Member), "Object is not of the expected type");
        }

        [TestMethod]
        public void Member_Create_WhenNameMinLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Member(new string('x', 2)));
        }

        [TestMethod]
        public void Member_Create_WhenNameMaxLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Member(new string('x', 24)));
        }

        [TestMethod]
        public void Member_Create_WhenNameNull_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Member(null));
        }

        [TestMethod]
        public void Member_Create_WhenNameWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Member(new string(' ', 10)));
        }

        [TestMethod]
        public void Member_AddActivityHistory_ShouldReturn()
        {
            var name = "TestMember";
            var description = "TestDescription";
            var member = new Member(name);

            member.AddActivity(description);

            Assert.AreEqual(2, member.ActivityHistory.Count);
            var log = member.ActivityHistory.LastOrDefault();
            Assert.AreEqual(description, log);
        }

        [TestMethod]
        public void Member_AddComment_ShouldAddComment()
        {
            var member = new Member("TestMember");
            var story = TestHelpers.GetTestStory();
            var comment = TestHelpers.InitializeTestComment();

            member.AddComment(comment, story);

            Assert.AreEqual(2, member.ActivityHistory.Count);
        }
    }
}
