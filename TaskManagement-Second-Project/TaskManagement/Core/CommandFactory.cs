using System;
using System.Collections.Generic;

using System.Text;
using TaskManagement.Exceptions;
using TaskManagement.Core.Contracts;
using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Commands.Enums;


namespace TaskManagement.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IRepository repository;

        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }

        public ICommand Create(string commandLine)
        {
            // RemoveEmptyEntries makes sure no empty strings are added to the result of the split operation.
            string[] arguments = commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            CommandType commandType = ParseCommandType(arguments[0]);
            List<string> commandParameters = ExtractCommandParameters(arguments);

            switch (commandType)
            {
                case CommandType.CreateMember:
                    return new CreateMemberCommand(commandParameters, repository);
                case CommandType.ListMembers:
                    return new ListMembersCommand(repository);
                case CommandType.CreateTeam:
                    return new CreateTeamCommand(commandParameters, repository);
                case CommandType.ListTeams:
                    return new ListTeamsCommand(repository);
                case CommandType.CreateStory:
                    return new CreateStoryCommand(commandParameters, repository);
                case CommandType.AddMemberToTeam:
                    return new AddMemberToTeamCommand(commandParameters, repository);
                case CommandType.ListTeamMembers:
                    return new ListTeamMembers(commandParameters, repository);
                case CommandType.CreateBoardInTeam:
                    return new CreateBoardInTeamCommand(commandParameters, repository);
                case CommandType.ListTeamBoards:
                    return new ListTeamBoardsCommand(repository);
                case CommandType.Help:
                    return new HelpCommand();
                case CommandType.CreateBug:
                    return new CreateBugCommand(commandParameters, repository);
                case CommandType.CreateFeedback:
                    return new CreateFeedbackCommand(commandParameters, repository);
                case CommandType.ChangeStorySize:
                    return new ChangeStorySizeCommand(commandParameters, repository);
                case CommandType.ChangeStoryPriority:
                    return new ChangeStoryPriorityCommand(commandParameters, repository);
                case CommandType.ChangeStoryStatus:
                    return new ChangeStoryStatusCommand(commandParameters, repository);
                case CommandType.ChangeFeedbackStatus:
                    return new ChangeFeedbackStatusCommand(commandParameters, repository);
                case CommandType.ChangeFeedbackRating:
                    return new ChangeFeedbackRatingCommand(commandParameters, repository);
                case CommandType.ChangeBugPriority:
                    return new ChangeBugPriorityCommand(commandParameters, repository);
                case CommandType.ChangeBugSeverity:
                    return new ChangeBugSeverityCommand(commandParameters, repository);
                case CommandType.ChangeBugStatus:
                    return new ChangeBugStatusCommand(commandParameters, repository);
                case CommandType.AddComment:
                    return new AddCommentCommand(commandParameters, repository);
                case CommandType.AssignTaskToMember:
                    return new AssignTaskToMemberCommand(commandParameters, repository);
                case CommandType.UnassignTaskToMember:
                    return new UnassignTaskToMemberCommand(commandParameters, repository);
                case CommandType.ListAllTasks:
                    return new ListAllTasksCommand(commandParameters, repository);
                case CommandType.ListBugByStatus:
                    return new ListBugByStatusCommand(commandParameters, repository);
                case CommandType.ListStoryByStatus:
                    return new ListStoryByStatusCommand(commandParameters, repository);
                case CommandType.ListFeedbackByStatus:
                    return new ListFeedbackByStatusCommand(commandParameters, repository);
                case CommandType.ListTasksByAssignee:
                    return new ListTasksByAssigneeCommand(commandParameters, repository);
                case CommandType.ShowHistoryInTask:
                    return new ShowHistoryInTaskCommand(commandParameters, repository);
                case CommandType.ShowTeamActivity:
                    return new ShowTeamActivityCommand(commandParameters, repository);
                case CommandType.ShowMemberActivity:
                    return new ShowMemberActivityCommand(commandParameters, repository);
                case CommandType.ShowBoardActivity:
                    return new ShowBoardActivityCommand(commandParameters, repository);
                default:
                    throw new InvalidOperationException($"Command with name: {commandType} doesn't exist!");
            }
        }

        // Attempts to parse CommandType from a given string value.
        // If successful, returns the command enum value, otherwise returns null
        private CommandType ParseCommandType(string value)
        {
            if (Enum.TryParse(value, true, out CommandType result))
            {
                return result;
            }
            throw new InvalidUserInputException("Command with this parameter does not exist. Please try with valid parameter or type Help for guidance.");
        }

        // Receives a full line and extracts the parameters that are needed for the command to execute.
        // For example, if the input line is "FilterBy Assignee John",
        // the method will return a list of ["Assignee", "John"].
        private List<String> ExtractCommandParameters(string[] arguments)
        {
            List<string> commandParameters = new List<string>();

            for (int i = 1; i < arguments.Length; i++)
            {
                commandParameters.Add(arguments[i]);
            }

            return commandParameters;
        }


    }
}
