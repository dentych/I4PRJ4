using System.Net.Sockets;
using CentralServer.Threading;
using System.Net;
using CentralServer.Messaging.Messages;

namespace CentralServer.Server
{
    class SocketServer : ThreadBase
    {
        private MainControl _main;

        private Socket _listener = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);


        public SocketServer(MainControl main)
        {
            _main = main;
        }

        protected override void Run()
        {
            Bind();
            Listen();
        }

        private void Bind()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            _listener.Bind(localEndPoint);
            _listener.Listen(100);
        }

        private void Listen()
        {
            while (true)
                SpawnClient(_listener.Accept());
        }

        private void SpawnClient(Socket handle)
        {
            var client = new ClientControl(_main);
            var connection = new SocketConnection(handle, client);
            var msg = new ConnectionEstablishedMsg(connection);

            client.Start();
            client.Send(ClientControl.E_CONNECTION_ESTABLISHED, msg);
        }
    }
}
