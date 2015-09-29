using CentralServer.Logging;
using CentralServer.Logging.Loggers;
using CentralServer.Server;

namespace CentralServer
{
    class CentralServerMain
    {
        static void Main(string[] args)
        {
            var log = new Log(new ConsoleLogger());

            log.Write("Main", "Initiating");

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
    }
}
