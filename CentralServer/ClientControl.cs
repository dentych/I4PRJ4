using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Server;
using SharedLib.Protocol;

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

        private Log _log;
        private MainControl _main;
        private SocketConnection _connection;
        private Protocol _protocol = new Protocol();
        private long _sessionId;


        public ClientControl(Log log, MainControl main)
        {
            _log = log;
            _main = main;
        }

        protected override void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_CONNECTION_ESTABLISHED:
                    _log.Write(this, "Recieved E_CONNECTION_ESTABLISHED");
                    HandleConnectionEstablished((ConnectionEstablishedMsg)msg);
                    break;
                case E_CONNECTION_CLOSED:
                    _log.Write(this, "Recieved E_CONNECTION_CLOSED");
                    HandleConnectionClosed();
                    break;
                case E_WELCOME:
                    _log.Write(this, "Recieved WELCOME");
                    HandleWelcome((WelcomeMsg)msg);
                    break;
                case E_DATA_RECIEVED:
                    _log.Write(this, "Recieved E_DATA_RECIEVED");
                    HandleDataRecieved((DataRecievedMsg)msg);
                    break;
                case E_SEND_COMMAND:
                    _log.Write(this, "Recieved E_SEND_COMMAND");
                    HandleSendCommand((SendCommandMsg)msg);
                    break;
                default:
                    _log.Write(this, "Recieved unknown event ID: " + id);
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
            _protocol.AddData(msg.Data);
            
            foreach(var cmd in _protocol.GetCommands())
            {
                var cmsg = new CommandRecievedMsg(_sessionId, cmd);
                _main.Send(MainControl.E_COMMAND_RECIEVED, cmsg);
            }
        }

        private void HandleSendCommand(SendCommandMsg msg)
        {
            _connection.Send(_protocol.Encode(msg.Command));
        }
    }
}
