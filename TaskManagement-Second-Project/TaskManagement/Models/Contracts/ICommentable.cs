using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }

        void AddComment(IComment comment);

    }
}
