using System;

namespace CentralServer.Logging.Loggers
{
    class ConsoleLog : ILogger
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
