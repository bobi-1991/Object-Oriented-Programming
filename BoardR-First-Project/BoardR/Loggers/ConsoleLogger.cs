using System;
using System.IO;

namespace BoardR.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string value)
        {
            Console.WriteLine(value);
            var result = $"{new string('=', 10)} {DateTime.Now.ToString("dd-MM-yyyy")} " +
                $" {new string('=', 10)} {Environment.NewLine}{value} {Environment.NewLine} " +
                $" {new string('=', 30)} {Environment.NewLine}";

            File.AppendAllText("C:\\Users\\Strahil\\Desktop\\Logs.txt", result);
        }
    }
}
