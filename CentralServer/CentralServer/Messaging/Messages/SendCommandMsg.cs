using CentralServer.Protocol;

namespace CentralServer.Messaging.Messages
{
    class SendCommandMsg : Message
    {
        private Command _command;

        public Command Command { get { return _command; } }

        public SendCommandMsg(Command command)
        {
            _command = command;
        }
    }
}
