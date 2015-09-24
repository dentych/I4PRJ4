
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
            _logger.Write(text);
        }
    }
}
