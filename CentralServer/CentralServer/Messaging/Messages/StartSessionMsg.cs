
namespace CentralServer.Messaging.Messages
{
    /*
     * SocketServer registers a new SocketClient.
     * 
     * Sender: SocketServer
     * Reciever: Controller
     */
    public class StartSessionMsg : Message
    {
        private IMessageReceiver _client;
        public IMessageReceiver Client { get { return _client; } }

        public StartSessionMsg(IMessageReceiver client)
        {
            _client = client;
        }
    }
}
