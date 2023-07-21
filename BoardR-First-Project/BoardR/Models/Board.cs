using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Text;
using BoardR.Loggers;

namespace BoardR
{
    public static class Board
    {
        private readonly static List<BoardItem> Items = new List<BoardItem>();

        public static int TotalItems
        {
            get { return Items.Count; }
        }
        public static void AddItem(BoardItem item)
        {
            if (Items.Contains(item))
            {
                throw new InvalidOperationException($"{item} already exists");
            }
            Items.Add(item);
        }
        public static void LogHistory(ILogger logger)
        {
            foreach (BoardItem item in Items)
            {
                logger.Log(item.ViewHistory());
            }
        }
    }
}
