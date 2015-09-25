using System;

namespace CentralServer.Logging.Loggers
{
    class ConsoleLogger : ILogger
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
