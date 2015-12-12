
namespace CentralServer.Messaging.Messages
{
    /*
     * SocketServer unregisters a SocketClient.
     * 
     * Sender: SocketClient
     * Reciever: Controller
     */
    public class StopSessionMsg : Message
    {
        private long _sessionId;
        public long SessionId { get { return _sessionId; } }

        public StopSessionMsg(long sessionId)
        {
            _sessionId = sessionId;
        }
    }
}
