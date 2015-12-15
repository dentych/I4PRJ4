using System;

namespace CentralServer.Logging
{
    public class Log : ILog
    {
        public const int DEBUG = 0;
        public const int NOTICE = 1;
        public const int WARNING = 2;
        public const int ERROR = 3;

        private ILogger _logger;
        private int _level;

        public Log(ILogger logger, int level = NOTICE)
        {
            _logger = logger;
            _level = level;
        }

        /// <summary>
        /// Write to the log.
        /// </summary>
        /// <param name="sender">Name of the class which writes to the log</param>
        /// <param name="category">The level of logging applied</param>
        /// <param name="text">Logging text</param>
        public void Write(string sender, int category, string text)
        {
            if (category >= _level)
                _logger.Write(sender, category, text, DateTime.Now.ToString());
        }
    }
}
