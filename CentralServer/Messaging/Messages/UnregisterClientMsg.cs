
namespace CentralServer.Messaging.Messages
{
    /*
     * SocketServer unregisters a SocketClient.
     * 
     * Sender: SocketClient
     * Reciever: Controller
     */
    class UnregisterClientMsg : Message
    {
        private long _sessionId;
        public long SessionId { get { return _sessionId; } }

        public UnregisterClientMsg(long sessionId)
        {
            _sessionId = sessionId;
        }
    }
}
