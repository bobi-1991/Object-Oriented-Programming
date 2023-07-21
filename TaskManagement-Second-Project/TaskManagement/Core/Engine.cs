using System;
using System.Collections.Generic;
using System.Text;

using TaskManagement.Core.Contracts;
using TaskManagement.Commands.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "exit";
        private const string EmptyCommandError = "Command cannot be empty.";
        private const string SharpsSeparator = "#########################";
             

        private readonly ICommandFactory commandFactory;

        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void Start()
        {
            Console.WriteLine("If you need a list of supported Commands, please type Help ");
            while (true)
            {
                try
                {
                    string inputLine = Console.ReadLine().Trim();

                    if (inputLine == string.Empty)
                    {
                        Console.WriteLine(EmptyCommandError);
                        continue;
                    }

                    if (inputLine.ToLower() == TerminationCommand)
                    {
                        break;
                    }

                    ICommand command = commandFactory.Create(inputLine);
                    string result = command.Execute();
                    Console.WriteLine(result.Trim());
                    Console.WriteLine(SharpsSeparator);
                }
                catch (InvalidUserInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(AuthorizationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //catch (EntityNotFoundException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
