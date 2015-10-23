
namespace CentralServer.Messaging.Messages
{
    /*
     * SocketServer registers a new SocketClient.
     * 
     * Sender: SocketServer
     * Reciever: Controller
     */
    class StartSessionMsg : Message
    {
        private ClientControl _client;
        public ClientControl Client { get { return _client; } }

        public StartSessionMsg(ClientControl client)
        {
            _client = client;
        }
    }
}
