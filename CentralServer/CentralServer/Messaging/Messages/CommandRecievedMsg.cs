using SharedLib.Protocol;

namespace CentralServer.Messaging.Messages
{
    public class CommandRecievedMsg : Message
    {
        private long _sessionId;
        private Command _command;

        public long SessionId { get { return _sessionId; } }
        public Command Command { get { return _command; } }

        public CommandRecievedMsg(long sessionId, Command command)
        {
            _sessionId = sessionId;
            _command = command;
        }
    }
}
