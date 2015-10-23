using CentralServer.Logging;
using CentralServer.Logging.Loggers;
using CentralServer.Server;

namespace CentralServer
{
    class CentralServerMain
    {
        static void Main(string[] args)
        {
            var log = new Log(new ConsoleColorLogger(), Log.DEBUG);

            LogCheck(log);
            log.Write("Main", Log.NOTICE, "Initiating");

            var main = new MainControl(log);
            var server = new SocketServer(log, main, GetServerPort(args));

            main.Start();
            server.Start();

            main.Join();
            server.Join();
        }

        private static int GetServerPort(string[] args)
        {
            return 11000;
        }

        private static void LogCheck(Log log)
        {
            log.Write("LogCheck", Log.ERROR, "Category: ERROR");
            log.Write("LogCheck", Log.WARNING, "Category: WARNING");
            log.Write("LogCheck", Log.NOTICE, "Category: NOTICE");
            log.Write("LogCheck", Log.DEBUG, "Category: DEBUG");
        }
    }
}
