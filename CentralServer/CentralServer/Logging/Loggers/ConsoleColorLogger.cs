using System;

namespace CentralServer.Logging.Loggers
{
    class ConsoleColorLogger : ILogger
    {
        private object mutex = new Object();


        /// <summary>
        /// Write to the console.
        /// </summary>
        /// <param name="sender">Name of the class which writes to the log</param>
        /// <param name="category">The level of logging applied</param>
        /// <param name="text">Logging text</param>
        /// <param name="timestamp">Current timestamp</param>
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

        /// <summary>
        /// Returns an appropriate color for a category
        /// </summary>
        /// <param name="category">Logging category</param>
        /// <returns>The color to write to console</returns>
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
