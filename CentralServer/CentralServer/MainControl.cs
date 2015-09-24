using CentralServer.Messaging;
using CentralServer.Messaging.Messages;

namespace CentralServer
{
    class MainControl : MessageThread
    {
        // A new client requests to be registered
        public const long E_REGISTER_CLIENT = 1;
        // A known clients requests to be unregistered
        public const long E_UNREGISTER_CLIENT = 2;
        // A command was recieved from a known client
        public const long E_COMMAND_RECIEVED = 3;

        private SessionControl _sessions = new SessionControl();


        protected override void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_REGISTER_CLIENT:
                    HandleRegisterClient((RegisterClientMsg)msg);
                    break;
                case E_UNREGISTER_CLIENT:
                    HandleUnregisterClient((UnregisterClientMsg)msg);
                    break;
                case E_COMMAND_RECIEVED:
                    HandleCommandReieved((CommandRecievedMsg)msg);
                    break;
            }
        }

        private void HandleRegisterClient(RegisterClientMsg msg)
        {
            var client = msg.Client;
            var sessionId = _sessions.Register(client);
            var response = new WelcomeMsg(sessionId);
            client.Send(ClientControl.E_WELCOME, response);
        }

        private void HandleUnregisterClient(UnregisterClientMsg msg)
        {
            _sessions.Unregister(msg.SessionId);
        }


        // Handle commands recieved from clients

        private void HandleCommandReieved(CommandRecievedMsg msg)
        {
            var client = _sessions.GetClient(msg.SessionId);
            var cmd = msg.Command;

            switch (cmd.Name)
            {
                case "GetCatalogue":
                    OnGetCatalogue(client, (GetCatalogue)cmd);
                    break;
                case "CreateProduct":
                    OnCreateProduct(client, (CreateProduct)cmd);
                    break;
                case "RegisterPurchase":
                    OnRegisterPurchase(client, (RegisterPurchase)cmd);
                    break;
            }
        }

        private void OnGetCatalogue(ClientControl client, GetCatalogue cmd)
        {

        }

        private void OnCreateProduct(ClientControl client, CreateProduct cmd)
        {

        }

        private void OnRegisterPurchase(ClientControl client, RegisterPurchase cmd)
        {

        }
    }
}
