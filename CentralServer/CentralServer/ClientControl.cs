using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Server;

namespace CentralServer
{
    class ClientControl : MessageThread
    {
        // Socket connection has been established
        public const long E_CONNECTION_ESTABLISHED = 1;
        // Socket connection has been closed by client
        public const long E_CONNECTION_CLOSED = 2;
        // Client has been registered
        public const long E_WELCOME = 3;
        // Recieved (raw) data from socket connection
        public const long E_DATA_RECIEVED = 4;
        // Main control requests to send a command to client
        public const long E_SEND_COMMAND = 5;

        private MainControl _main;
        private SocketConnection _connection;
        private long _sessionId;


        public ClientControl(MainControl main)
        {
            _main = main;
        }

        protected override void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_CONNECTION_ESTABLISHED:
                    HandleConnectionEstablished((ConnectionEstablishedMsg)msg);
                    break;
                case E_CONNECTION_CLOSED:
                    HandleConnectionClosed();
                    break;
                case E_WELCOME:
                    HandleWelcome((WelcomeMsg)msg);
                    break;
                case E_DATA_RECIEVED:
                    HandleDataRecieved((DataRecievedMsg)msg);
                    break;
                case E_SEND_COMMAND:
                    HandleSendCommand((SendCommandMsg)msg);
                    break;
            }
        }

        private void HandleConnectionEstablished(ConnectionEstablishedMsg msg)
        {
            _connection = msg.Connection;
            var registerMsg = new RegisterClientMsg(this);
            _main.Send(MainControl.E_REGISTER_CLIENT, registerMsg);
        }

        private void HandleConnectionClosed()
        {
            var unregisterMsg = new UnregisterClientMsg(_sessionId);
            _main.Send(MainControl.E_UNREGISTER_CLIENT, unregisterMsg);
            _connection = null;
            Abort();
        }

        private void HandleWelcome(WelcomeMsg msg)
        {
            _sessionId = msg.SessionId;
            _connection.StartRecieving();
        }

        private void HandleDataRecieved(DataRecievedMsg msg)
        {
        }

        private void HandleSendCommand(SendCommandMsg msg)
        {
            var cmd = msg.Command;
        }
    }
}
