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
            var s = String.Format("[{0}] {1}", DateTime.Now, text);
            _logger.Write(s);
        }
    }
}
