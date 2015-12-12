using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Server;
using SharedLib.Protocol;
using System;

namespace CentralServer
{
    public class ClientControl : IMessageHandler
    {
        // Client has been registered
        public const long E_WELCOME = 1;
        // Recieved (raw) data from socket connection
        public const long E_DATA_RECEIVED = 2;
        // Main control requests to send a command to client
        public const long E_SEND_COMMAND = 3;

        private ILog _log;
        private IMessageReceiver _main;
        private ISocketConnection _connection;
        private IProtocol _protocol = new Protocol();
        private long _sessionId;


        public ClientControl(ILog log, IMessageReceiver main, ISocketConnection conn, IProtocol protocol)
        {
            _log = log;
            _main = main;
            _connection = conn;
            _protocol = protocol;

            _connection.OnDataRecieved += HandleDataRecieved;
            _connection.OnDisconnect += HandleConnectionClosed;
        }

        public void Dispatch(long id, Message msg)
        {
            if (_connection == null)
                throw new StopHandling();

            switch (id)
            {
                case E_WELCOME:
                    _log.Write("ClientControl", Log.DEBUG, "Recieved E_WELCOME");
                    HandleWelcome((WelcomeMsg)msg);
                    break;
                case E_SEND_COMMAND:
                    _log.Write("ClientControl", Log.DEBUG, "Recieved E_SEND_COMMAND");
                    HandleSendCommand((SendCommandMsg)msg);
                    break;
            }
        }

        private void HandleConnectionClosed()
        {
            _connection = null;

            _log.Write("ClientControl", Log.NOTICE, "Connection closed");

            var unregisterMsg = new StopSessionMsg(_sessionId);
            _main.Send(MainControl.E_STOP_SESSION, unregisterMsg);
        }

        private void HandleDataRecieved(string data)
        {
            _log.Write("ClientControl", Log.DEBUG, "Parsing data");

            _protocol.AddData(data);

            try
            {
                var commands = _protocol.GetCommands();

                foreach (var cmd in commands)
                {
                    var cmsg = new CommandRecievedMsg(_sessionId, cmd);
                    _main.Send(MainControl.E_COMMAND_RECEIVED, cmsg);
                }
            }
            catch (Exception e)
            {
                _log.Write("ClientControl", Log.ERROR, 
                           "Could not parse data: " + data);
                throw e;
            }
        }

        private void HandleWelcome(WelcomeMsg msg)
        {
            _sessionId = msg.SessionId;
            _connection.StartRecieving();
        }

        private void HandleSendCommand(SendCommandMsg msg)
        {
            if (_connection != null)
                _connection.Send(_protocol.Encode(msg.Command));
        }
    }
}
