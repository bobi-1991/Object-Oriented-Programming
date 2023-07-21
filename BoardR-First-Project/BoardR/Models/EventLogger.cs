using System;
using System.Collections.Generic;

namespace BoardR
{
    public static class EventLogger
    {
        public static List<EventLog> logs = new List<EventLog>();

        public static void ViewHistory()
        {
            foreach (var item in logs)
            {
                Console.WriteLine($"[{item.Time.ToString("yyyyMMdd|HH:mm:ss.ffff")}]{item.Description}");
            }
        }
    }
}
