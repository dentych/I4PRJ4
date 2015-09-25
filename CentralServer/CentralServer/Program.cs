using CentralServer.Logging;
using CentralServer.Logging.Loggers;
using CentralServer.Server;

namespace CentralServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new Log(new ConsoleLogger());
            var main = new MainControl();
            var server = new SocketServer(main);

            main.Start();
            server.Start();

            main.Join();
            server.Join();
        }
    }
}
