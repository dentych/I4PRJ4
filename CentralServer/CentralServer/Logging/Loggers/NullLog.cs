
using System;

namespace CentralServer.Logging.Loggers
{
    class NullLog : ILogger
    {
        public void Write(string text)
        {
        }
    }
}
