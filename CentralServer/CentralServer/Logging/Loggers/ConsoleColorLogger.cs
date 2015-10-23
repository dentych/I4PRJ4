using System;

namespace CentralServer.Logging.Loggers
{
    class ConsoleColorLogger : ILogger
    {
        private object mutex = new Object();

        public void Write(string sender, int category, string text, string timestamp)
        {
            lock (mutex)
            {
                Console.Write("[{0}] (", timestamp);
                Console.ForegroundColor = GetCategoryColor(category);
                Console.Write(sender);
                Console.ResetColor();
                Console.Write(") {0}\n", text);
            }
        }

        private ConsoleColor GetCategoryColor(int category)
        {
            switch (category)
            {
                case Log.ERROR:
                    return ConsoleColor.DarkRed;
                case Log.WARNING:
                    return ConsoleColor.DarkYellow;
                case Log.DEBUG:
                    return ConsoleColor.DarkCyan;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
