using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Sessions;
using CentralServer.Handlers;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace CentralServer
{
    public class MainControl : IMessageHandler
    {
        // A new client requests to be registered
        public const long E_START_SESSION = 1;
        // A known clients requests to be unregistered
        public const long E_STOP_SESSION = 2;
        // A command was recieved from a known client
        public const long E_COMMAND_RECIEVED = 3;

        private readonly ILog _log;
        private readonly ISessionControl _sessions;
        private readonly ICommandHandler _handler;


        public MainControl(ILog log, ISessionControl sessions, ICommandHandler handler)
        {
            _log = log;
            _sessions = sessions;
            _handler = handler;
        }


        /*
         * Invoked when this thread recieves a new message.
         * Messages propagate to specific event handlers.
         */
        public void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_START_SESSION:
                    _log.Write("MainControl", Log.DEBUG,
                               "Recieved E_START_SESSION");
                    HandleStartSession((StartSessionMsg)msg);
                    break;
                case E_STOP_SESSION:
                    _log.Write("MainControl", Log.DEBUG,
                               "Recieved E_STOP_SESSION");
                    HandleStopSession((StopSessionMsg)msg);
                    break;
                case E_COMMAND_RECIEVED:
                    _log.Write("MainControl", Log.DEBUG,
                               "Recieved E_COMMAND_RECIEVED");
                    HandleCommandReieved((CommandRecievedMsg)msg);
                    break;
                default:
                    _log.Write("MainControl", Log.DEBUG,
                               "Recieved unknown event ID: " + id);
                    break;
            }
        }


        /*
         * Invoked when MainControl accepts a client.
         * Register a new session and respond with Session ID to the client.
         */
        private void HandleStartSession(StartSessionMsg msg)
        {
            var client = msg.Client;
            var sessionId = _sessions.Register(client);
            var response = new WelcomeMsg(sessionId);

            _log.Write("MainControl", Log.DEBUG,
                       "New client registered. Session ID: " + sessionId);

            client.Send(ClientControl.E_WELCOME, response);
        }


        /*
         * Invoked when a client has disconnected.
         * In this case its session must be unregistered.
         */
        private void HandleStopSession(StopSessionMsg msg)
        {
            _sessions.Unregister(msg.SessionId);
        }


        /*
         * Invoked whenever a clients sends a command to the server.
         */
        private void HandleCommandReieved(CommandRecievedMsg msg)
        {
            var cmd = msg.Command;
            var client = _sessions.GetClient(msg.SessionId);

            _log.Write("MainControl", Log.DEBUG,
                       "Recieved command: " + cmd.CmdName);

            // Invoke the appropriate handler according to the command recieved
            switch (cmd.CmdName)
            {
                // Catalogue
                case "GetCatalogue":
                    _handler.HandleGetCatalogue(client, (GetCatalogueCmd)cmd);
                    break;
                
                // Products
                case "CreateProduct":
                    _handler.HandleCreateProduct(client, (CreateProductCmd)cmd);
                    break;
                case "EditProduct":
                    _handler.HandleEditProduct(client, (EditProductCmd)cmd);
                    break;
                case "DeleteProduct":
                    _handler.HandleDeleteProduct(client, (DeleteProductCmd)cmd);
                    break;

                // Product Categories
                case "CreateProductCategory":
                    _handler.HandleCreateProductCategory(client, (CreateProductCategoryCmd)cmd);
                    break;
                case "EditProductCategory":
                    _handler.HandleEditProductCategory(client, (EditProductCategoryCmd)cmd);
                    break;
                case "DeleteProductCategory":
                    _handler.HandleDeleteProductCategory(client, (DeleteProductCategoryCmd)cmd);
                    break;

                // Purchases
                case "RegisterPurchase":
                    _handler.HandleRegisterPurchase(client, (RegisterPurchaseCmd)cmd);
                    break;
            }
        }
    }
}
