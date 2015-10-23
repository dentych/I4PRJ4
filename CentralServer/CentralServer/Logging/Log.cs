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
            Write(sender.GetType().Name, text);
        }

        public void Write(string sender, string text)
        {
            var timestamp = DateTime.Now;
            var s = String.Format("[{0}] ({1}): {2}", timestamp, sender, text);
            _logger.Write(s);
        }
    }
}
