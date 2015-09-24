using CentralServer.Server;

namespace CentralServer.Messaging.Messages
{
    class ConnectionEstablishedMsg : Message
    {
        private SocketConnection _connection;
        public SocketConnection Connection { get { return _connection; } }

        public ConnectionEstablishedMsg(SocketConnection connection)
        {
            _connection = connection;
        }
    }
}
