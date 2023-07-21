using BoardR;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BoardRTests
{
    [TestClass]
    public class IssueTests
    {
        private const string Title = "This is a test title";
        private const string Description = "TestDescription";
        private DateTime DueDate = new DateTime(2030, 01, 01);

        [TestMethod]
        public void Property_WhenTitleMinLength_ShouldThrow()
        {
            //Arrange
            var title = new string('x', 2);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Issue(title, Description, DueDate));

            //Assert
            Assert.AreEqual("Title is outside the requirements for Length", ex.Message);
        }

        [TestMethod]
        public void Property_WhenTitleMaxLength_ShouldThrow()
        {
            //Arrange
            var title = new string('x', 50);

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Issue(title, Description, DueDate));

            //Assert
            Assert.AreEqual("Title is outside the requirements for Length", ex.Message);
        }

        [TestMethod]
        public void Property_WhenTitleNull_ShouldThrow()
        {
            //Arrange
            string title = null;

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Issue(title, Description, DueDate));

            //Assert
            Assert.AreEqual("Title cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Property_WhenTitleEmpty_ShouldThrow()
        {
            //Arrange
            string title = string.Empty;

            //Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Issue(title, Description, DueDate));

            //Assert
            Assert.AreEqual("Title cannot be null or empty.", ex.Message);
        }

        [TestMethod]
        public void Property_WhenTitleChanged_ShouldLog()
        {
            //Arrange
            var issue = new Issue(Title, Description, DueDate);

            //Act
            var newTitle = "changedTitle";
            issue.Title = newTitle;

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual($"Title changed from {Title} to {newTitle}", log.Description);
        }

        [TestMethod]
        public void Property_WhenDateNotInFuture_ShouldThrow()
        {
            //Arrange && Act
            var ex = Assert.ThrowsException<ArgumentException>(
                () => new Issue(Title, Description, DateTime.Now));

            //Assert
            Assert.AreEqual("Date must be in the future", ex.Message);
        }

        [TestMethod]
        public void Property_WhenDateChanged_ShouldLog()
        {
            //Arrange
            var issue = new Issue(Title, Description, DueDate);

            //Act
            var newDate = DueDate.AddDays(1);
            issue.DueDate = newDate;

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual($"DueDate changed from '{DueDate.ToString("dd-M-yyyy")}' to '{newDate.ToString("dd-M-yyyy")}'", log.Description);
        }

        [TestMethod]
        public void Constructor_WhenDescriptionNull_ShouldSet()
        {
            //Arrange && Act
            string newDescription = null;
            var issue = new Issue(Title, newDescription, DueDate);

            //Assert
            Assert.AreEqual("No description", issue.Description);
        }

        [TestMethod]
        public void Constructor_WhenAllDataValid_ShouldCreate()
        {
            //Arrange && Act
            var issue = new Issue(Title, Description, DueDate);

            //Assert
            Assert.AreEqual(Title, issue.Title);
            Assert.AreEqual(Description, issue.Description);
            Assert.AreEqual(DueDate, issue.DueDate);
        }

        [TestMethod]
        public void AdvanceStatus_ShouldAdvance()
        {
            //Arrange && Act
            var issue = new Issue(Title, Description, DueDate);
            Assert.AreEqual(Status.Open, issue.Status);
            issue.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issue.Status);

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual("Issue status set to Verified", log.Description);
        }

        [TestMethod]
        public void AdvanceStatus_ShouldNotAdvance_WhenVerified()
        {
            //Arrange && Act
            var issue = new Issue(Title, Description, DueDate);
            Assert.AreEqual(Status.Open, issue.Status);
            issue.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issue.Status);
            issue.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issue.Status);

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(3, logs.Count);
            Assert.AreEqual("Issue status already Verified", log.Description);
        }

        [TestMethod]
        public void RevertStatus_ShouldRevert()
        {
            //Arrange && Act
            var issue = new Issue(Title, Description, DueDate);
            Assert.AreEqual(Status.Open, issue.Status);
            issue.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issue.Status);
            issue.RevertStatus();
            Assert.AreEqual(Status.Open, issue.Status);

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(3, logs.Count);
            Assert.AreEqual("Issue status set to Open", log.Description);
        }

        [TestMethod]
        public void RevertStatus_ShouldNotRevert()
        {
            //Arrange && Act
            var issue = new Issue(Title, Description, DueDate);
            Assert.AreEqual(Status.Open, issue.Status);
            issue.AdvanceStatus();
            Assert.AreEqual(Status.Verified, issue.Status);
            issue.RevertStatus();
            Assert.AreEqual(Status.Open, issue.Status);
            issue.RevertStatus();
            Assert.AreEqual(Status.Open, issue.Status);

            //Assert
            var logs = issue.logs;
            var log = logs.LastOrDefault();
            Assert.AreEqual(4, logs.Count);
            Assert.AreEqual("Issue status already Open", log.Description);
        }

        [TestMethod]
        public void ViewInfo_ShouldReturnResult()
        {
            //Arrange
            var issue = new Issue(Title, Description, DueDate);
            string formatDate = DueDate.ToString("dd-M-yyyy");

            //Act
            var actualResult = issue.ViewInfo();

            //Assert
            Assert.AreEqual($"Issue: '{Title}', [{issue.Status}|{formatDate}]. Description: {Description}", actualResult);
        }
    }
}
