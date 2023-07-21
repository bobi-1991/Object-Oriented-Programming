using TaskManagement.Models.Enums;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Helper;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class StoryTests
    {
        public const string Title = "StoryTestTitle";
        public const string Description = "StoryDescriptionTest";

        [TestMethod]
        public void Story_ShouldImplement_IStoryInterface()
        {
            var type = typeof(Story);
            var isAssignable = typeof(IStory).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Story does not implement IStory interface");
        }

        [TestMethod]
        public void Story_ShouldImplement_ICommentableInterface()
        {
            var type = typeof(Story);
            var isAssignable = typeof(ICommentable).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Story does not implement ICommentable interface");
        }

        [TestMethod]
        public void Story_ShouldImplement_ITaskBaseInterface()
        {
            var type = typeof(Story);
            var isAssignable = typeof(ITaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Story does not implement ITaskBase interface");
        }

        [TestMethod]
        public void Story_Should_DeriveFromTaskBase()
        {
            var type = typeof(Story);
            var isAssignable = typeof(TaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Story class does not derive from TaskBase base class!");
        }

        [TestMethod]
        public void Story_Create_WhenAllParametersAreValid()
        {
            var story = TestHelpers.GetTestStory();

            Assert.IsInstanceOfType(story, typeof(Story), "Object is not of the expected type");
        }

        [TestMethod]
        public void Story_Create_WhenTitleMinLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(new string('x', 2), Description, Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenTitleWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(new string(' ', 10), Description, Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenTitleNull_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(null, Description, Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenTitleMaxLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(new string('x', 51), Description, Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenDescriptionMaxLenght_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(Title, new string('x', 501), Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenDescriptionMinLenght_ShouldThrow()
        {
           Assert.ThrowsException<InvalidUserInputException>(() => new Story(Title, new string('x', 2), Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenDescriptionNull_ShouldThrow()
        {
           Assert.ThrowsException<InvalidUserInputException>(() => new Story(Title, null, Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenDescriptionWhiteSpace_ShouldThrow()
        {
            Assert.ThrowsException<InvalidUserInputException>(() => new Story(Title, new string(' ', 10), Priority.High, StorySize.Large));
        }

        [TestMethod]
        public void Story_Create_WhenPriorityValid_ShouldSet()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(Priority.Medium, story.Priority);
        }

        [TestMethod]
        public void Story_Create_WhenSizeValid_ShouldSet()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StorySize.Small, story.Size);
        }

        [TestMethod]
        public void Story_Create_StoryStatus_ShouldSet()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StoryStatus.NotDone, story.Status);
        }

        [TestMethod]
        public void AdvanceStatus_ShouldAdvanceStatus()
        {
            var story = TestHelpers.GetTestStory();
           
            Assert.AreEqual(StoryStatus.NotDone, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.InProgress, story.Status);
        }

        [TestMethod]
        public void RevertStatus_ShouldRevertStatus()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StoryStatus.NotDone, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.InProgress, story.Status);
            story.RevertStatus();
            Assert.AreEqual(StoryStatus.NotDone, story.Status);
        }

        [TestMethod]
        public void RevertStatus_ShouldNotRevert()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StoryStatus.NotDone, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.InProgress, story.Status);
            story.RevertStatus();
            Assert.AreEqual(StoryStatus.NotDone, story.Status);
            story.RevertStatus();
            Assert.AreEqual(StoryStatus.NotDone, story.Status);
        }

        [TestMethod]
        public void RevertStatus_ShouldNotAdvance()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StoryStatus.NotDone, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.InProgress, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.Done, story.Status);
            story.AdvanceStatus();
            Assert.AreEqual(StoryStatus.Done, story.Status);
        }

        [TestMethod]
        public void AdvanceSize_ShouldAdvance()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StorySize.Small, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Medium, story.Size);
        }

        [TestMethod]
        public void RevertSize_ShouldRevert()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StorySize.Small, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Medium, story.Size);
            story.RevertSize();
            Assert.AreEqual(StorySize.Small, story.Size);
        }

        [TestMethod]
        public void RevertSize_ShouldNotRevert()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StorySize.Small, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Medium, story.Size);
            story.RevertSize();
            Assert.AreEqual(StorySize.Small, story.Size);
            story.RevertSize();
            Assert.AreEqual(StorySize.Small, story.Size);
        }

        [TestMethod]
        public void AdvanceSize_ShouldNotAdvance()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(StorySize.Small, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Medium, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Large, story.Size);
            story.AdvanceSize();
            Assert.AreEqual(StorySize.Large, story.Size);
        }

        [TestMethod]
        public void AdvancePriority_ShouldAdvance()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(Priority.Medium, story.Priority);
            story.AdvancePriority();
            Assert.AreEqual(Priority.High, story.Priority);
        }

        [TestMethod]
        public void AdvancePriority_ShouldNotAdvance()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(Priority.Medium, story.Priority);
            story.AdvancePriority();
            Assert.AreEqual(Priority.High, story.Priority);
            story.AdvancePriority();
            Assert.AreEqual(Priority.High, story.Priority);
        }

        [TestMethod]
        public void RevertPriority_ShouldRevert()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(Priority.Medium, story.Priority);
            story.RevertPriority();
            Assert.AreEqual(Priority.Low, story.Priority);
        }

        [TestMethod]
        public void RevertPriority_ShouldNotRevert()
        {
            var story = TestHelpers.GetTestStory();

            Assert.AreEqual(Priority.Medium, story.Priority);
            story.RevertPriority();
            Assert.AreEqual(Priority.Low, story.Priority);
            story.RevertPriority();
            Assert.AreEqual(Priority.Low, story.Priority);
        }

        [TestMethod]
        public void Assignee_Should_ReturnNull_When_CreateStory()
        {
            var story = TestHelpers.GetTestStory();
            var expectedAssignee = story.Assignee;

            Assert.AreEqual(expectedAssignee, story.Assignee);
        }

        [TestMethod]
        public void Assignee_Should_ReturnCorrectAssignee_When_AssigneeIsAdded()
        {
            var story = TestHelpers.GetTestStory();
            var member = new Member("TestMember");

            story.AddAssignee(member);
            var expectedAssignee = member;

            Assert.AreEqual(expectedAssignee, story.Assignee);
        }

        [TestMethod]
        public void Unassign_Should_UnassignAssignee()
        {
            var story = TestHelpers.GetTestStory();
            var member = new Member("TestMember");

            story.AddAssignee(member);
            story.Unassign();

            Assert.IsNull(story.Assignee);
        }
    }
}
