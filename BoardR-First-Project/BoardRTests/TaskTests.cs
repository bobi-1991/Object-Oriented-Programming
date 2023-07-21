using BoardR;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BoardRTests
{
    [TestClass]
    public class TaskTests
    {
        private const string Title = "This is a test title";
        private const string Assignee = "TestUser";
        private DateTime DueDate = new DateTime(2030, 01, 01);

        [TestMethod]
        public void Constructor_SetCorrectParameters()
        {
            //Act
            var task = new Task(Title, Assignee, DueDate);

            //Assert
            Assert.AreEqual(Title, task.Title);
            Assert.AreEqual(Assignee, task.Assignee);
            Assert.AreEqual(DueDate, task.DueDate);
        }

        [DataRow("")]
        [TestMethod]
        public void Constructor_WithEmptyAssignee_ShouldThrow(string assignee)
        {
            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(Title, assignee, DueDate));

            //Assert
            Assert.AreEqual("Assignee cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Constructor_WithNullAssignee_ShouldThrow()
        {
            //string assignee = null;
            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(Title, null, DueDate));

            //Assert
            Assert.AreEqual("Assignee cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Constructor_WithMinLengthAssignee_ShouldThrow()
        {
            //Arrange
            string assignee = new string('a', 4);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(Title, assignee, DueDate));

            //Assert
            Assert.AreEqual("Assignee cannot be less than 5 and more than 30", ex.Message);
        }

        [TestMethod]
        public void Constructor_WithMaxLengthAssignee_ShouldThrow()
        {
            //Arrange
            string assignee = new string('a', 50);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(Title, assignee, DueDate));

            //Assert
            Assert.AreEqual("Assignee cannot be less than 5 and more than 30", ex.Message);
        }

        [TestMethod]
        public void Constructor_WhenChangingAssignee_ShouldLogMessage()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act
            var newAssignee = "ChangedAssignee";
            task.Assignee = newAssignee;

            //Assert
            Assert.AreEqual(newAssignee, task.Assignee);
            var logs = task.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual($"Assignee changed from {Assignee} to {newAssignee}", log.Description);
        }

        [TestMethod]
        public void AdvanceStatus_ShouldAdvanceStatus()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act && Assert
            Assert.AreEqual(Status.ToDo, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.InProgress, task.Status);

            Assert.AreEqual(2, task.logs.Count);
            var log = task.logs.LastOrDefault();
            Assert.AreEqual("Task changed from ToDo to InProgress", log.Description);
        }

        [TestMethod]
        public void AdvanceStatus_StatusVerified_ShouldThrow()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act && Assert
            Assert.AreEqual(Status.ToDo, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.InProgress, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Done, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Verified, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.Verified, task.Status);

            Assert.AreEqual(5, task.logs.Count);
            var log = task.logs.LastOrDefault();
            Assert.AreEqual("Task status already at Verified", log.Description);
        }

        [TestMethod]
        public void RevertStatus_ShouldRevert()
        {
            var task = new Task(Title, Assignee, DueDate);

            Assert.AreEqual(Status.ToDo, task.Status);
            task.AdvanceStatus();
            Assert.AreEqual(Status.InProgress, task.Status);

            task.RevertStatus();
            Assert.AreEqual(Status.ToDo, task.Status);

            var logs = task.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(3, logs.Count);
            Assert.AreEqual("Task changed from InProgress to ToDo", log.Description);
        }

        [TestMethod]
        public void RevertStatus_WhenStatusToDo_ShouldNotRevert()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act
            Assert.AreEqual(Status.ToDo, task.Status);
            task.RevertStatus();

            //Assert
            var logs = task.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual("Task status already ToDo", log.Description);
        }

        [TestMethod]
        public void ViewInfo_ShouldReturnResult()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);
            string formatDate = DueDate.ToString("dd-M-yyyy");

            //Act
            var actualResult = task.ViewInfo();

            //Assert
            Assert.AreEqual($"Task: '{Title}', [{task.Status}|{formatDate}] Assignee: {Assignee}", actualResult);
        }

        [TestMethod]
        public void Constructor_WhenTitleNull_ShouldThrow()
        {
            //Arrange
            string title = null;

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(title, Assignee, DueDate));

            //Assert
            Assert.AreEqual("Title cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Constructor_WhenTitleEmpty_ShouldThrow()
        {
            //Arrange
            string title = string.Empty;

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(title, Assignee, DueDate));

            //Assert
            Assert.AreEqual("Title cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Constructor_WithMinLengthTitle_ShouldThrow()
        {
            //Arrange
            string title = new string('a', 4);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(title, Assignee, DueDate));

            //Assert
            Assert.AreEqual("Title is outside the requirements for Length", ex.Message);
        }

        [TestMethod]
        public void Constructor_WithMaxLengthTitle_ShouldThrow()
        {
            //Arrange
            string title = new string('a', 50);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(title, Assignee, DueDate));

            //Assert
            Assert.AreEqual("Title is outside the requirements for Length", ex.Message);
        }

        [TestMethod]
        public void Constructor_WhenChangingTitle_ShouldLog()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act
            var newTitle = "ChangedTitle";
            task.Title = newTitle;

            //Assert
            Assert.AreEqual(newTitle, task.Title);
            var logs = task.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual($"Title changed from {Title} to {newTitle}", log.Description);
        }

        [TestMethod]
        public void Constructor_WhenDateNotInFuture_ShouldThrow()
        {
            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Task(Title, Assignee, DateTime.Now));

            //Assert
            Assert.AreEqual("Date must be in the future", ex.Message);
        }

        [TestMethod]
        public void Constructor_WhenChangingDate_ShouldLog()
        {
            //Arrange
            var task = new Task(Title, Assignee, DueDate);

            //Act
            var newDate = DueDate.AddDays(1);
            task.DueDate = newDate;
            var logs = task.logs;
            var log = logs.LastOrDefault();

            //Assert
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual($"DueDate changed from '{DueDate.ToString("dd-M-yyyy")}' to '{newDate.ToString("dd-M-yyyy")}'", log.Description);
        }
    }
}
