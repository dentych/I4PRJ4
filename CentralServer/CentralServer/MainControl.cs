using CentralServer.Logging;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using ServerDatabase;
using SharedLib.Models;
using SharedLib.Protocol.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralServer
{
    class MainControl : MessageThread
    {
        // A new client requests to be registered
        public const long E_START_SESSION = 1;
        // A known clients requests to be unregistered
        public const long E_STOP_SESSION = 2;
        // A command was recieved from a known client
        public const long E_COMMAND_RECIEVED = 3;

        private Log _log;
        private SessionControl _sessions = new SessionControl();


        public MainControl(Log log)
        {
            _log = log;
        }

        /*
         * Invoked when this thread recieves a new message
         */
        protected override void Dispatch(long id, Message msg)
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
         * 
         */
        private void HandleStartSession(StartSessionMsg msg)
        {
            var client = msg.Client;
            var sessionId = _sessions.Register(client);
            var response = new WelcomeMsg(sessionId);

            client.Send(ClientControl.E_WELCOME, response);

            _log.Write("MainControl", Log.DEBUG,
                       "New client registered. Session ID: " + sessionId);
        }

        private void HandleStopSession(StopSessionMsg msg)
        {
            _sessions.Unregister(msg.SessionId);
        }


        // Handle commands recieved from clients

        private void HandleCommandReieved(CommandRecievedMsg msg)
        {
            var cmd = msg.Command;
            var client = _sessions.GetClient(msg.SessionId);
            _log.Write("MainControl", Log.DEBUG,
                       "Recieved command: " + cmd.CmdName);

            switch (cmd.CmdName)
            {
                case "GetCatalogue":
                    OnGetCatalogue(client, (GetCatalogueCmd)cmd);
                    break;
                case "CreateProduct":
                    OnCreateProduct(client, (CreateProductCmd)cmd);
                    break;
                case "RegisterPurchase":
                    OnRegisterPurchase(client, (RegisterPurchaseCmd)cmd);
                    break;
            }
        }

        private void OnGetCatalogue(ClientControl client, GetCatalogueCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client recieving catalogue details");

            var catalogueCmd = new CatalogueDetailsCmd();

            using (var db = new DatabaseContext())
            {
                var query = from p in db.Products select p;
                foreach (var product in query)
                    catalogueCmd.Products.Add(product);
            }

            var response = new SendCommandMsg(catalogueCmd);
            client.Send(ClientControl.E_SEND_COMMAND, response);
        }

        private void OnCreateProduct(ClientControl client, CreateProductCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client creating a new product");
            
            using (var db = new DatabaseContext())
            {
                var product = new Product()
                {
                    Name = cmd.Name,
                    ProductNumber = cmd.ProductNumber,
                    Price = cmd.Price,
                };

                db.Products.Add(product);
                db.SaveChanges();
            }

        }

        private void OnRegisterPurchase(ClientControl client, RegisterPurchaseCmd cmd)
        {
            _log.Write("MainControl", Log.NOTICE,
                       "Client registering a new purchase");
        }
    }
}
