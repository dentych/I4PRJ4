﻿using System.Net.Sockets;
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

        private Socket _listener = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);


        public SocketServer(Log log, MainControl main)
        {
            _log = log;
            _main = main;
        }

        protected override void Run()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            _listener.Bind(localEndPoint);
            _listener.Listen(100);

            while (true)
                SpawnClient(_listener.Accept());
        }

        private void SpawnClient(Socket handle)
        {
            var client = new ClientControl(_main);
            var connection = new SocketConnection(_log, handle, client);
            var msg = new ConnectionEstablishedMsg(connection);

            client.Start();
            client.Send(ClientControl.E_CONNECTION_ESTABLISHED, msg);
        }
    }
}
