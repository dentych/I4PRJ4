using System;

namespace CentralServer.Logging.Loggers
{
    class ConsoleLogger : ILogger
    {
        private object mutex = new Object();

        public void Write(string sender, int category, string text, string timestamp)
        {
            lock (mutex)
            {
                Console.Write(sender);
            }
        }
    }
}
