using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Helper;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class FeedbackTests
    {
        public const string Title = "FeedbackTestTitle";
        public const string Description = "FeedbackDescriptionTest";

        [TestMethod]
        public void Feedback_ShouldImplement_IFeedbackInterface()
        {
            var type = typeof(Feedback);
            var isAssignable = typeof(IFeedback).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Feedback does not implement IFeedback interface");
        }

        [TestMethod]
        public void Feedback_ShouldImplement_ICommentableInterface()
        {
            var type = typeof(Feedback);
            var isAssignable = typeof(ICommentable).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Feedback does not implement ICommentable interface");
        }

        [TestMethod]
        public void Feedback_ShouldImplement_ITaskBaseInterface()
        {
            var type = typeof(Feedback);
            var isAssignable = typeof(ITaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Feedback does not implement ITaskBase interface");
        }

        [TestMethod]
        public void Feedback_Should_DeriveFromTaskBase()
        {
            var type = typeof(Feedback);
            var isAssignable = typeof(TaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Feedback class does not derive from TaskBaseS base class!");
        }

        [TestMethod]
        public void Feedback_Create_WhenAllParametersAreValid()
        {
            var feedback = TestHelpers.GetTestFeedback();

            Assert.IsInstanceOfType(feedback, typeof(Feedback), "Object is not of the expected type");
        }

        [TestMethod]
        public void Feedback_Create_WhenTitleMinLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(new string('x', 2), Description, 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenTitleMaxLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(new string('x', 52), Description, 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenTitleWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(new string(' ', 10), Description, 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenTitleNull_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(null, Description, 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenDescriptionMaxLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, new string('x', 502), 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenDescriptionMinLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, new string('x', 2), 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenDescriptionWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, new string(' ', 10), 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenDescriptionNull_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, null, 8));
        }

        [TestMethod]
        public void Feedback_Create_WhenRatingMin_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, Description, 0));
        }

        [TestMethod]
        public void Feedback_Create_WhenRatingMax_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Feedback(Title, Description, 11));
        }

        [TestMethod]
        public void Feedback_Create_VerifyIfStatusValid_ShouldReturn()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
        }

        [TestMethod]
        public void Feedback_AdvanceStatus_ShouldAdvance()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Unscheduled, feedback.Status);
        }

        [TestMethod]
        public void Feedback_AdvanceStatus_ShouldNotAdvance()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Unscheduled, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Scheduled, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Done, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Done, feedback.Status);
        }

        [TestMethod]
        public void Feedback_RevertStatus_ShouldRevert()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Unscheduled, feedback.Status);
            feedback.RevertStatus();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
        }

        [TestMethod]
        public void Feedback_RevertStatus_ShouldNotRevert()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
            feedback.AdvanceStatus();
            Assert.AreEqual(FeedBackStatus.Unscheduled, feedback.Status);
            feedback.RevertStatus();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
            feedback.RevertStatus();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);
        }

        [TestMethod]
        public void Feedback_ChangeRating_ShouldChange()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);

            feedback.ChangeRating(7);
            Assert.AreEqual(7, feedback.Rating);
        }

        [TestMethod]
        public void Feedback_ChangeRating_ShouldNotChange()
        {
            var feedback = TestHelpers.GetTestFeedback();
            Assert.AreEqual(FeedBackStatus.New, feedback.Status);

            var ex = Assert.ThrowsException<InvalidUserInputException>(() => feedback.ChangeRating(11));
        }
    }
}
