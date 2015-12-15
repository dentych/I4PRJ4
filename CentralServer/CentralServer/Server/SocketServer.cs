using System.Net.Sockets;
using System.Net;
using CentralServer.Threading;
using CentralServer.Messaging.Messages;
using CentralServer.Logging;
using CentralServer.Messaging;
using SharedLib.Protocol;

namespace CentralServer.Server
{
    /// <summary>
    /// Implements a socket server, which listens for incoming connections
    /// and accepts them.
    /// </summary>
    class SocketServer : IThreadRunner
    {
        private ILog _log;
        private IMessageReceiver _mainControl;
        private int _port;

        private Socket _listener = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);


        public SocketServer(ILog log, IMessageReceiver mainControl, int port)
        {
            _log = log;
            _mainControl = mainControl;
            _port = port;
        }

        /// <summary>
        /// Run the server
        /// </summary>
        public void RunThread()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _port);

            _listener.Bind(localEndPoint);
            _listener.Listen(100);

            _log.Write("SocketServer", Log.NOTICE,
                       "Listening on port " + _port);

            while (true)
                SpawnClient(_listener.Accept());
        }

        /// <summary>
        /// Invoked when a new connection has been accepted.
        /// Spawns a new ClientControl object and registers the client with MainControl.
        /// </summary>
        /// <param name="handle">The Socket object</param>
        private void SpawnClient(Socket handle)
        {
            _log.Write("SocketServer", Log.DEBUG,
                       "Spawning new socket client");

            var connection = new SocketConnection(_log, handle);

            // Init ClientControl and start thread
            var clientControl = new ClientControl(_log, _mainControl, connection, new Protocol());
            var clientReceiver = new MessageReceiver(clientControl, new MessageQueue());
            var clientThread = ThreadStarter.Start(clientReceiver);

            RegisterClient(clientReceiver);
        }

        /// <summary>
        /// Registers a ClientControl with MainControl
        /// </summary>
        /// <param name="client">The ClientControl object</param>
        private void RegisterClient(IMessageReceiver client)
        {
            var registerMsg = new StartSessionMsg(client);
            _mainControl.Send(MainControl.E_START_SESSION, registerMsg);
        }
    }
}
