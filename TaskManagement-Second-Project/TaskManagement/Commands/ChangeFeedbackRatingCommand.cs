using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ChangeFeedbackRatingCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 2;

        public ChangeFeedbackRatingCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedNumberOfArguments || this.CommandParameters.Count > ExpectedNumberOfArguments)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
            }

            int id = ParseIntParameter(CommandParameters[0], "id");
            int rating = ParseIntParameter(CommandParameters[1], "rating");

            return this.ChangeRating(id, rating);
        }
        private string ChangeRating(int id, int rating)
        {
            var feedback = this.Repository.GetFeedback(id);

            int oldFeedbackRating = feedback.Rating;
            feedback.ChangeRating(rating);

            return $"Rating was changed from {oldFeedbackRating} to {rating}";
        }
    }
}
