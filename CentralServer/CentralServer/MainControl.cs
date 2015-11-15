using System.Linq;
using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Sessions;
using ServerDatabase;
using SharedLib.Models;
using SharedLib.Protocol;
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


        public MainControl(ILog log, ISessionControl sessionControl)
        {
            _log = log;
            _sessions = sessionControl;
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
                case "GetCatalogue":
                    OnGetCatalogue(client, (GetCatalogueCmd)cmd);
                    break;

                case "CreateProduct":
                    OnCreateProduct(client, (CreateProductCmd)cmd);
                    break;
                case "EditProduct":
                    OnEditProduct(client, (EditProductCmd)cmd);
                    break;
                case "DeleteProduct":
                    OnDeleteProduct(client, (DeleteProductCmd)cmd);
                    break;
                case "CreateProductCategory":
                    OnCreateProductCategory(client, (CreateProductCategoryCmd) cmd);
                    break;
                case "DeleteProductCategory":
                    OnDeleteProductCategory(client, (DeleteProductCategoryCmd) cmd);
                    break;
                case "EditProductCategory":
                    OnEditProductCategory(client, (EditProductCategoryCmd) cmd);
                    break;
                case "RegisterPurchase":
                    OnRegisterPurchase(client, (RegisterPurchaseCmd)cmd);
                    break;
            }
        }
        
        /*
         * A client requests to recieve the entire product catalogue.
         * Respond with a CatalogueDetails command.
         */
        private void OnGetCatalogue(IMessageReceiver client, GetCatalogueCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client recieving catalogue details");

            var catalogueCmd = new CatalogueDetailsCmd();

            // Retrieve products from database
            using (var db = new DatabaseContext())
            {
                var query = from pc in db.ProductCategories select pc;

                foreach (var category in query)
                {
                    catalogueCmd.ProductCategories.Add(category);
                }

                foreach (var category in catalogueCmd.ProductCategories)
                {
                    var products = from p in db.Products
                                   where p.ProductCategoryId.Equals(category.ProductCategoryId)
                                   select p;
                    category.Products = products.ToList();
                }
            }

            // Send response command
            var response = new SendCommandMsg(catalogueCmd);
            client.Send(ClientControl.E_SEND_COMMAND, response);
        }

        /*
         * A client requests to create a new product.
         * Create the product in database and notify all connected clients.
         */
        private void OnCreateProduct(IMessageReceiver client, CreateProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client creating a new product");

            // Create product
            var product = new Product()
            {
                Name = cmd.Name,
                ProductNumber = cmd.ProductNumber,
                Price = cmd.Price,
                ProductCategoryId = cmd.ProductCategoryId
            };

            // Write to database
            using (var db = new DatabaseContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            // Broadcast changes to all connected clients
            Broadcast(new ProductCreatedCmd(product));
        }


        private void OnEditProduct(IMessageReceiver client, EditProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client modifying an existing product");

            Product product;
            int oldProductCategoryId;

            using (var db = new DatabaseContext())
            {
                product = db.Products.Find(cmd.ProductId);

                if (product == null)
                    return;

                oldProductCategoryId = product.ProductCategoryId;

                product.Name = cmd.Name;
                product.ProductNumber = cmd.ProductNumber;
                product.Price = cmd.Price;
                product.ProductCategoryId = cmd.ProductCategoryId;

                db.Entry(product).CurrentValues.SetValues(product);
                db.SaveChanges();
            }

            Broadcast(new ProductEditedCmd(product, oldProductCategoryId));
        }


        private void OnDeleteProduct(IMessageReceiver client, DeleteProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client deleting a product");

            Product product;

            using (var db = new DatabaseContext())
            {
                product = db.Products.Find(cmd.ProductId);

                if (product == null)
                    return;

                db.Products.Remove(product);
                db.SaveChanges();
            }

            Broadcast(new ProductDeletedCmd(product));
        }

        private void OnEditProductCategory(IMessageReceiver client, EditProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                   "Client modifying an existing productcategory");

            ProductCategory cat;

            using (var db = new DatabaseContext())
            {
                cat = db.ProductCategories.Find(cmd.ProductCategoryId);

                if (cat == null)
                    return;

                cat.Name = cmd.Name;
                db.Entry(cat).CurrentValues.SetValues(cat);
                db.SaveChanges();
            }

            Broadcast(new ProductCategoryEditedCmd(cat));
        }

        private void OnDeleteProductCategory(IMessageReceiver client, DeleteProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
           "Client deleting a productcategory");

            ProductCategory cat;

            using (var db = new DatabaseContext())
            {
                cat = db.ProductCategories.Find(cmd.ProductCategoryId);

                if (cat == null)
                    return;

                db.ProductCategories.Remove(cat);
                db.SaveChanges();
            }

            Broadcast(new DeleteProductCategoryCmd(cat));
        }

        private void OnCreateProductCategory(IMessageReceiver client, CreateProductCategoryCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                    "Client creating a new productCategory");

            // Create product
            var cat = new ProductCategory()
            {
                Name = cmd.Name,
            };

            // Write to database
            using (var db = new DatabaseContext())
            {
                db.ProductCategories.Add(cat);
                db.SaveChanges();
            }

            // Broadcast changes to all connected clients
            Broadcast(new ProductCategoryCreatedCmd(cat));
        }


        /*
         * Invoked when a clients wants to register a purchase
         */
        private void OnRegisterPurchase(IMessageReceiver client, RegisterPurchaseCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client registering a new purchase");
        }

        /*
         * Broadcast a command to all connected clients
         */
        private void Broadcast(Command cmd)
        {
            foreach (var c in _sessions.GetClients())
            {
                var msg = new SendCommandMsg(cmd);
                c.Send(ClientControl.E_SEND_COMMAND, msg);
            }
        }
    }
}
