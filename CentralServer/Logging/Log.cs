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

        public void Write(object sender, string text)
        {
            var timestamp = DateTime.Now;
            var senderName = sender.GetType().Name;
            var s = String.Format("[{0}] ({1}): {2}", timestamp, senderName, text);
            _logger.Write(s);
        }
    }
}
