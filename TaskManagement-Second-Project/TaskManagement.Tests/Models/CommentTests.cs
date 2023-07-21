using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class CommentTests
    {

        [TestMethod]
        public void Comment_ShouldImplement_ICommentInterface()
        {
            var type = typeof(Comment);
            var isAssignable = typeof(IComment).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Comment does not implement IComment interface");
        }

        [TestMethod]
        public void Comment_WhenValidParameters_ShouldCreate()
        {
            var content = new string('x', 3);
            var author = new string('x', 5);

            var comment = new Comment(content, author);

            Assert.AreEqual(content, comment.Content);
            Assert.AreEqual(author, comment.Author);
            Assert.IsInstanceOfType(comment, typeof(Comment), "Object is not of the expected type");
        }

        [TestMethod]
        public void Comment_WhenContentMinLength_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Comment(new string('x', 2), new string('x', 5)));
        }

        [TestMethod]
        public void Comment_WhenContentMaxLength_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Comment(new string('x', 201), new string('x', 5)));
        }

        [TestMethod]
        public void Comment_WhenAuthorNull_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Comment(new string('x', 3), null));
        }

        [TestMethod]
        public void Comment_WhenAuthorWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Comment(new string('x', 3), new string(' ', 10)));
        }
    }
}
