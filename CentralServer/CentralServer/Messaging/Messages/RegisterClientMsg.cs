
namespace CentralServer.Messaging.Messages
{
    /*
     * SocketServer registers a new SocketClient.
     * 
     * Sender: SocketServer
     * Reciever: Controller
     */
    class RegisterClientMsg : Message
    {
        private ClientControl _client;
        public ClientControl Client { get { return _client; } }

        public RegisterClientMsg(ClientControl client)
        {
            _client = client;
        }
    }
}
