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
            var mainReciever = new MessageReceiver(main, new MessageQueue());

            // Init socket server
            var port = GetServerPort(args);
            var serverRunner = new SocketServer(log, mainReciever, port);

            // Start all threads
            var mainThread = ThreadStarter.Start(mainReciever);
            var serverThread = ThreadStarter.Start(serverRunner);

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
