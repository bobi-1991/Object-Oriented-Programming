using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface ITeam
    {
        string Name { get; }
        List<IMember> Members { get; }
        List<IBoard> Boards { get; }
        void AddBoard(IBoard board);
        void AddMember(IMember member);
    }
}
