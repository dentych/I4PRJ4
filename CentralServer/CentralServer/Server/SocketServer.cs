using System.Net.Sockets;
using System.Net;
using CentralServer.Threading;
using CentralServer.Messaging.Messages;
using CentralServer.Logging;

namespace CentralServer.Server
{
    class SocketServer : ThreadBase
    {
        private Log _log;
        private MainControl _main;
        private int _port;

        private Socket _listener = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);


        public SocketServer(Log log, MainControl main, int port)
        {
            _log = log;
            _main = main;
            _port = port;
        }

        protected override void Run()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            _listener.Bind(localEndPoint);
            _listener.Listen(100);

            _log.Write(this, "Listening on " + localEndPoint);

            while (true)
            {
                SpawnClient(_listener.Accept());
                _log.Write(this, "Connection accepted");
            }
        }

        private void SpawnClient(Socket handle)
        {
            var connection = new SocketConnection(_log, handle);
            var client = new ClientControl(_log, connection, _main);
            var msg = new ConnectionEstablishedMsg();

            client.Start();
            client.Send(ClientControl.E_CONNECTION_ESTABLISHED, msg);
        }
    }
}
