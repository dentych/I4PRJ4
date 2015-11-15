using CentralServer.Logging;
using CentralServer.Logging.Loggers;
using CentralServer.Messaging;
using CentralServer.Server;
using CentralServer.Sessions;
using CentralServer.Threading;

namespace CentralServer
{
    class CentralServerMain
    {
        static void Main(string[] args)
        {
            var log = new Log(new ConsoleColorLogger(), Log.DEBUG);

            LogCheck(log);
            log.Write("Main", Log.NOTICE, "Initiating");

            // Init MainControl
            var sessions = new SessionControl();
            var main = new MainControl(log, sessions);
            var mainRunner = new MessageReceiver(main, new MessageQueue());

            // Init socket server
            var port = GetServerPort(args);
            var server = new SocketServer(log, mainRunner, port);

            // Start all threads
            var mainThread = ThreadStarter.Start(mainRunner);
            var serverThread = ThreadStarter.Start(server);

            // Join on all threads
            mainThread.Join();
            serverThread.Join();
        }

        private static int GetServerPort(string[] args)
        {
            return 11000;
        }

        private static void LogCheck(ILog log)
        {
            log.Write("LogCheck", Log.ERROR, "Category: ERROR");
            log.Write("LogCheck", Log.WARNING, "Category: WARNING");
            log.Write("LogCheck", Log.NOTICE, "Category: NOTICE");
            log.Write("LogCheck", Log.DEBUG, "Category: DEBUG");
        }
    }
}
