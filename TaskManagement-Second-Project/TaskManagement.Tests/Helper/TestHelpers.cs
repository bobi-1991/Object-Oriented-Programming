using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts;
using TaskManagement.Core;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models;
using System.Drawing;

namespace TaskManagement.Tests.Helper
{
    public class TestHelpers
    {
        public static List<string> GetListWithSize(int size)
        { 
        return new string[size].ToList();
        }
        public static IRepository GetTestRepository()
        {
            return new Repository();
        }

        public static ITeam GetTestTeam()
        {
           return new Team("TestTeam");
        }
        public static IBoard GetTestBoard()
        {
            return new Board("TestBoard");
        }
        public static IMember GetTestMember()
        {
            return new Member("TestMember");
        }

        public static IBug GetTestBug()
        {
            return new Bug(
            new string('x', TaskBase.TitleMinLength),
            new string('x', TaskBase.DescriptionMinLength),
            Priority.Medium,
            BugSeverity.Minor,
            new List<string>() {"test","steps"});
            
        }
        public static IStory GetTestStory()
        {
            return new Story(
            new string('x', TaskBase.TitleMinLength),
            new string('x', TaskBase.DescriptionMinLength),
            Priority.Medium,
            StorySize.Small);
        }
        public static IFeedback GetTestFeedback()
        {
            return new Feedback(
            new string('x', TaskBase.TitleMinLength),
            new string('x', TaskBase.DescriptionMinLength),
            Feedback.RatingMinValue);
        }
        public static IComment InitializeTestComment()
        {
            return new Comment(
               new string ('x', Comment.CommentMinLength),
               new string ('x',5));
        }
 
    }
}
