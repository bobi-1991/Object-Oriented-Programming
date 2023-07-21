using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Helper;

namespace TaskManagement.Tests.Models
{
    [TestClass]
    public class BugTests
    {

        [TestMethod]
        public void Bug_ShouldImplement_IBugInterface()
        {
            var type = typeof(Bug);
            var isAssignable = typeof(IBug).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Bug does not implement IBug interface");
        }

        [TestMethod]
        public void Bug_ShouldImplement_ICommentableInterface()
        {
            var type = typeof(Bug);
            var isAssignable = typeof(ICommentable).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Bug does not implement ICommentable interface");
        }

        [TestMethod]
        public void Bug_ShouldImplement_ITaskBaseInterface()
        {
            var type = typeof(Bug);
            var isAssignable = typeof(ITaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Bug does not implement ITaskBase interface");
        }

        [TestMethod]
        public void Bug_Should_DeriveFromTaskBase()
        {
            var type = typeof(Bug);
            var isAssignable = typeof(TaskBase).IsAssignableFrom(type);

            Assert.IsTrue(isAssignable, "Bug class does not derive from TaskBaseS base class!");
        }

        [TestMethod]
        public void Steps_Should_ReturnCorrectString_When_BugIsAdded()
        {
            IBug bug = TestHelpers.GetTestBug();
            List<string> expectedSteps = new List<string>() {"test", "steps"};

            CollectionAssert.AreEqual(expectedSteps, bug.Steps.ToList());
        }

        [TestMethod]
        public void Priority_Should_ReturnCorrectInformation_When_BugIsAdded()
        {
            IBug bug = TestHelpers.GetTestBug();
            var bugPriority = bug.Priority;
            var expectedPriority = Priority.Medium;

            Assert.AreEqual(expectedPriority, bug.Priority);
        }

        [TestMethod]
        public void BugSeverity_Should_ReturnCorrectInformation_When_BugIsAdded()
        {
            IBug bug = TestHelpers.GetTestBug();
            var bugSeverity = bug.Severity;
            var expectedBugSeverity = BugSeverity.Minor;

            Assert.AreEqual(expectedBugSeverity, bug.Severity);
        }

        [TestMethod]
        public void BugStatus_Should_ReturnCorrectInformation_When_BugIsAdded()
        {
            IBug bug = TestHelpers.GetTestBug();
            var bugStatus = bug.Status;
            var expectedBugStatus = BugStatus.Active;

            Assert.AreEqual(expectedBugStatus, bug.Status);
        }

        [TestMethod]
        public void AdvanceStatus_Should_AdvanceStatus()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedStatus = BugStatus.Fixed;

            bug.AdvanceStatus();
            Assert.AreEqual(expectedStatus, bug.Status);
        }

        [TestMethod]
        public void AdvanceStatus_Should_NotChanged_When_StatusIsFixed()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedStatus = BugStatus.Fixed;

            bug.AdvanceStatus();
            bug.AdvanceStatus();
            Assert.AreEqual(expectedStatus, bug.Status);
        }

        [TestMethod]
        public void RevertStatus_Should_RevertStatus()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedStatus = BugStatus.Active;

            bug.AdvanceStatus();
            bug.RevertStatus();
            Assert.AreEqual(expectedStatus, bug.Status);
        }

        [TestMethod]
        public void RevertStatus_Should_CannotRevert_When_StatusIsActive()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedStatus = BugStatus.Active;

            bug.RevertStatus();
            Assert.AreEqual(expectedStatus, bug.Status);
        }

        [TestMethod]
        public void AdvanceSeverity_Should_AdvanceBugSeverity()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedSeverity = BugSeverity.Major;

            bug.AdvanceSeverity();
            Assert.AreEqual(expectedSeverity, bug.Severity);
        }

        [TestMethod]
        public void AdvanceSeverity_Should_CannotAdvance_When_BugSeverityIsCritical()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedSeverity = BugSeverity.Critical;

            bug.AdvanceSeverity();
            bug.AdvanceSeverity();
            bug.AdvanceSeverity();
            bug.AdvanceSeverity();
            Assert.AreEqual(expectedSeverity, bug.Severity);
        }

        [TestMethod]
        public void RevertSeverity_Should_RevertSeverity()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedSeverity = BugSeverity.Minor;

            bug.AdvanceSeverity();
            bug.RevertSeverity();
            Assert.AreEqual(expectedSeverity, bug.Severity);
        }

        [TestMethod]
        public void RevertSeverity_Should_CannotRevert_When_BugSeverityIsMinor()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedSeverity = BugSeverity.Minor;

            bug.RevertSeverity();
            Assert.AreEqual(expectedSeverity, bug.Severity);
        }

        [TestMethod]
        public void AdvancePriority_Should_AdvancePriority()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedPriority = Priority.High;

            bug.AdvancePriority();
            Assert.AreEqual(expectedPriority, bug.Priority);
        }

        [TestMethod]
        public void AdvancePriority_Should_CannotAdvance_WhenPriorityIsHigh()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedPriority = Priority.High;

            bug.AdvancePriority();
            bug.AdvancePriority();
            Assert.AreEqual(expectedPriority, bug.Priority);
        }

        [TestMethod]
        public void RevertPriority_Should_RevertPriority()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedPriority = Priority.Low;

            bug.RevertPriority();
            Assert.AreEqual(expectedPriority, bug.Priority);
        }

        [TestMethod]
        public void RevertPriority_Should_CannotRevert_WhenPriorityIsLow()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedPriority = Priority.Low;

            bug.RevertPriority();
            bug.RevertPriority();
            Assert.AreEqual(expectedPriority, bug.Priority);
        }

        [TestMethod]
        public void Assignee_Should_ReturnNull_When_CreateBug()
        {
            var bug = TestHelpers.GetTestBug();
            var expectedAssignee = bug.Assignee;

            Assert.AreEqual(expectedAssignee, bug.Assignee);
        }

        [TestMethod]
        public void Assignee_Should_ReturnCorrectAssignee_When_AssigneeIsAdded()
        {
            var bug = TestHelpers.GetTestBug();
            var member = new Member("TestMember");

            bug.AddAssignee(member);

            Assert.AreEqual(member, bug.Assignee);
        }

        [TestMethod]
        public void Unassign_Should_UnassignAssignee()
        {
            var bug = TestHelpers.GetTestBug();
            var member = new Member("TestMember");

            bug.AddAssignee(member);
            bug.Unassign();

            Assert.AreEqual(null, bug.Assignee);
        }
    }
}
