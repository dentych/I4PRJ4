using System;

namespace CentralServer.Logging
{
    class Log
    {
        private ILogger _logger;

        public Log(ILogger logger)
        {
            _logger = logger;
        }

        public void Write(string text)
        {
            DateTime now = DateTime.Now;
            var s = String.Format("[{0}] {1}", now, text);
            _logger.Write(s);
        }
    }
}
