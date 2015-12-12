
namespace CentralServer.Messaging.Messages
{
    /*
     * Controller informs SocketClient that it has been
     * registered and can start to communicate with Controller.
     * 
     * Sender: Controller
     * Reciever: SocketClient
     */
    public class WelcomeMsg : Message
    {
        private long _sessionId;
        public long SessionId { get { return _sessionId; } }

        public WelcomeMsg(long sessionId)
        {
            _sessionId = sessionId;
        }
    }
}
