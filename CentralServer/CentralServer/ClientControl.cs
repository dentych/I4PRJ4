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
        // Client has been registered
        public const long E_WELCOME = 2;
        // Recieved (raw) data from socket connection
        public const long E_DATA_RECIEVED = 3;
        // Main control requests to send a command to client
        public const long E_SEND_COMMAND = 4;

        private Log _log;
        private MainControl _main;
        private SocketConnection _connection;
        private Protocol _protocol = new Protocol();
        private long _sessionId;


        public ClientControl(Log log, SocketConnection conn, MainControl main)
        {
            _log = log;
            _main = main;
            _connection = conn;
            _connection.OnDataRecieved += HandleDataRecieved;
            _connection.OnDisconnect += HandleConnectionClosed;
        }

        protected override void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_CONNECTION_ESTABLISHED:
                    _log.Write("ClientControl", Log.DEBUG,
                               "Recieved E_CONNECTION_ESTABLISHED");
                    HandleConnectionEstablished();
                    break;
                case E_WELCOME:
                    _log.Write("ClientControl", Log.DEBUG,
                               "Recieved WELCOME");
                    HandleWelcome((WelcomeMsg)msg);
                    break;
                case E_SEND_COMMAND:
                    _log.Write("ClientControl", Log.DEBUG,
                               "Recieved E_SEND_COMMAND");
                    HandleSendCommand((SendCommandMsg)msg);
                    break;
                default:
                    _log.Write("ClientControl", Log.DEBUG,
                               "Recieved unknown event ID: " + id);
                    break;
            }
        }

        private void HandleConnectionEstablished()
        {
            var registerMsg = new StartSessionMsg(this);
            _main.Send(MainControl.E_START_SESSION, registerMsg);
        }

        private void HandleConnectionClosed()
        {
            _log.Write("ClientControl", Log.DEBUG,
                       "HandleConnectionClosed");
            var unregisterMsg = new StopSessionMsg(_sessionId);
            _main.Send(MainControl.E_STOP_SESSION, unregisterMsg);
            _connection = null;
            Abort();
        }

        private void HandleDataRecieved(string data)
        {
            _log.Write("ClientControl", Log.DEBUG,
                       "HandleDataRecieved");
            _protocol.AddData(data);

            foreach (var cmd in _protocol.GetCommands())
            {
                var cmsg = new CommandRecievedMsg(_sessionId, cmd);
                _main.Send(MainControl.E_COMMAND_RECIEVED, cmsg);
            }
        }

        private void HandleWelcome(WelcomeMsg msg)
        {
            _sessionId = msg.SessionId;
            _connection.StartRecieving();
        }

        private void HandleSendCommand(SendCommandMsg msg)
        {
            _connection.Send(_protocol.Encode(msg.Command));
        }
    }
}
