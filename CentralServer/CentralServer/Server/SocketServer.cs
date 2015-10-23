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
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);

            _listener.Bind(localEndPoint);
            _listener.Listen(100);

            _log.Write("SocketServer", Log.NOTICE,
                       "Listening on port " + _port);

            while (true)
            {
                SpawnClient(_listener.Accept());
                _log.Write("SocketServer", Log.NOTICE,
                           "Connection accepted");
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
