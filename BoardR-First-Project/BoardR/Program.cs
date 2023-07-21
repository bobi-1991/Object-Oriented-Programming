using BoardR.Loggers;
using System;

namespace BoardR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var tomorrow = DateTime.Now.AddDays(1);
            //var task = new Task("Write unit tests", "Peter", tomorrow);
            //var issue = new Issue("Review tests", "Someone must review Peter's tests.", tomorrow);

            //Board.AddItem(task);

            //Board.AddItem(issue);
            //task.AdvanceStatus();
            //issue.AdvanceStatus();

            //var history = Board.LogHistory();
            //var logger = new ConsoleLogger();
            //logger.Log(history);
            ////Board.LogHistory();

            //var tomorrow = DateTime.Now.AddDays(1);
            //var task = new Task("Write unit tests", "Peter", tomorrow);
            //var issue = new Issue("Review tests", "Someone must review Peter's tests.", tomorrow);
            //Board.AddItem(task);
            //Board.AddItem(issue);

            //ConsoleLogger logger = new ConsoleLogger();

            //Board.LogHistory(logger);

            //var tomorrow = DateTime.Now.AddDays(1);
            //BoardItem task = new Task("Write unit tests", "Peter", tomorrow);
            //Console.WriteLine(task.ViewInfo());

            //var tomorrow = DateTime.Now.AddDays(1);
            //BoardItem task = new Task("Write unit tests", "Peter", tomorrow);
            //BoardItem issue = new Issue("Review tests", "Someone must review Peter's tests.", tomorrow);

            //Console.WriteLine(task.ViewInfo());
            //Console.WriteLine(issue.ViewInfo());

            var tomorrow = DateTime.Now.AddDays(1);
            var task = new Task("Write unit tests", "Peter", tomorrow);
            var issue = new Issue("Review tests", "Someone must review Peter's tests.", tomorrow);
            Board.AddItem(task);
            Board.AddItem(issue);

            ConsoleLogger logger = new ConsoleLogger();
            Board.LogHistory(logger);


        }

        static void Print(string input)
        {
            Console.WriteLine(input);
        }
    }
}
