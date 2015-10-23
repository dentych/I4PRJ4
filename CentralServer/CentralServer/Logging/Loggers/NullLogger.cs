
using System;

namespace CentralServer.Logging.Loggers
{
    class NullLogger : ILogger
    {
        public void Write(string sender, int category, string text, string timestamp)
        {
            throw new NotImplementedException();
        }
    }
}
