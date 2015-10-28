using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Messaging;
using SharedLib.Messaging.Messages;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Sockets;

namespace SharedLib.Sockets
{
    public class SocketMsgReceiver : MessageThread
    {
        public const long E_DATA_RECIEVED = 0;

        public delegate void ProductCreatedHandler(Product product);
        public delegate void CatalogueDetailsHandler(Catalogue catalogue);

        public event ProductCreatedHandler OnProductCreated;
        public event CatalogueDetailsHandler OnCatalogueDetails;

        private SocketConnection _conn;
        private Protocol.Protocol _protocol = new Protocol.Protocol(); // FIXME: Namespace? WTF?


        public SocketMsgReceiver(SocketConnection conn)
        {
            _conn = conn;
            _conn.OnDataRecieved += HandleDataRecieved;
        }

        protected override void Dispatch(long id, Message msg)
        {
            switch (id)
            {
                case E_DATA_RECIEVED:
                    HandleDataRecieved((DataRecievedMsg) msg);
                    break;
            }
        }

        /*
         * Invoked by the socket connection whenever new data has been
         * read from socket. Is NOT syncronized with this thread!
         */
        private void HandleDataRecieved(string data)
        {
            Send(E_DATA_RECIEVED, new DataRecievedMsg(data));
        }

        /*
         * Invoked by Dispatch() whenever a DataRecievedMsg is recieved
         */
        private void HandleDataRecieved(DataRecievedMsg msg)
        {
            _protocol.AddData(msg.Data);

            foreach (var cmd in _protocol.GetCommands())
                HandleCommandRecieved(cmd);
        }

        private void HandleCommandRecieved(Command cmd)
        {
            switch (cmd.CmdName)
            {
                case "ProductCreated":
                    var product = ((ProductCreatedCmd)cmd).GetProduct();
                    OnProductCreated?.Invoke(product);
                    break;
                case "CatalogueDetails":
                    var catalogue = ((CatalogueDetailsCmd)cmd).GetCatalogue();
                    OnCatalogueDetails?.Invoke(catalogue);
                    break;
                default:
                    throw new Exception("Can not handle command: " + cmd.CmdName);
            }
        }

        public void SendCommand(Command cmd)
        {
            _conn.Send(_protocol.Encode(cmd));
        }
    }
}
