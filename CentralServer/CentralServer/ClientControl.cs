using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Server;
using SharedLib.Protocol;
using System;

namespace CentralServer
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientControl : IMessageHandler
    {
        // Client has been registered
        public const long E_WELCOME = 1;
        // Main control requests to send a command to client
        public const long E_SEND_COMMAND = 2;

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

        /// <summary>
        /// Dispatch an incoming message to the appropriate handler
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="msg">Message object</param>
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

        /// <summary>
        /// Invoked when the client closes the connection
        /// </summary>
        private void HandleConnectionClosed()
        {
            _connection = null;

            _log.Write("ClientControl", Log.NOTICE, "Connection closed");

            var unregisterMsg = new StopSessionMsg(_sessionId);
            _main.Send(MainControl.E_STOP_SESSION, unregisterMsg);
        }

        /// <summary>
        /// Invoked when data has been received by the client
        /// </summary>
        /// <param name="data">Raw data string</param>
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

        /// <summary>
        /// Invoked when this client has been registered with MainControl
        /// </summary>
        /// <param name="msg">Welcome message</param>
        private void HandleWelcome(WelcomeMsg msg)
        {
            _sessionId = msg.SessionId;
            _connection.StartRecieving();
        }

        /// <summary>
        /// Invoked when MainControl wants to send a command to this client
        /// </summary>
        /// <param name="msg">Message containing the command to send</param>
        private void HandleSendCommand(SendCommandMsg msg)
        {
            if (_connection != null)
                _connection.Send(_protocol.Encode(msg.Command));
        }
    }
}
