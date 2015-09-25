
using System;

namespace CentralServer.Logging.Loggers
{
    class NullLogger : ILogger
    {
        public void Write(string text)
        {
        }
    }
}
