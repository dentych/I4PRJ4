using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using SharedLib.Protocol.ProtocolMarshallers;
using System.Diagnostics;

namespace SharedLib.Sockets
{
    /// <summary>
    /// The prototype for the EventHandler for CommandReceiver
    /// </summary>
    /// <param name="cmd">The command received</param>
    public delegate void CommandRecievedHandler(Command cmd);
    /// <summary>
    /// The prototype for the EventHandler for Product Created
    /// </summary>
    /// <param name="cmd">The command with the product</param>
    public delegate void ProductCreatedHandler(ProductCreatedCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for new catalogue received
    /// </summary>
    /// <param name="cmd">the command with the catalogue</param>
    public delegate void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for Productcategory created
    /// </summary>
    /// <param name="cmd">The command with the new category</param>
    public delegate void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for product deleted
    /// </summary>
    /// <param name="cmd">cmd with the deleted product</param>
    public delegate void ProductDeletedHandler(ProductDeletedCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for Productcategory deleted
    /// </summary>
    /// <param name="cmd">Cmd with the deleted productcategory</param>
    public delegate void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for Productcategory edited
    /// </summary>
    /// <param name="cmd">Command with edited categor</param>
    public delegate void ProductCategoryEditedHandler(ProductCategoryEditedCmd cmd);
    /// <summary>
    /// The prototype for the EventHandler for Product edited
    /// </summary>
    /// <param name="cmd">The cmd containiing the edited product</param>
    public delegate void ProductEditedHandler(ProductEditedCmd cmd);

    /// <summary>
    /// The class the contains the logic for listening for commands.
    /// </summary>
    public class CommandListener
    {
        /// <summary>
        /// Event for command received.
        /// </summary>
        public event CommandRecievedHandler OnCommandRecieved;
        /// <summary>
        /// Event for  product created.
        /// </summary>
        public event ProductCreatedHandler OnProductCreated;
        /// <summary>
        /// Event for new catalogue details. 
        /// </summary>
        public event CatalogueDetailsHandler OnCatalogueDetails;
        /// <summary>
        /// Event for product deleted.
        /// </summary>
        public event ProductDeletedHandler OnProductDeleted;
        /// <summary>
        /// Event for category deleted.
        /// </summary>
        public event ProductCategoryDeletedHandler OnProductCategoryDeleted;
        /// <summary>
        /// Event for category created.
        /// </summary>
        public event ProductCategoryCreatedHandler OnProductCategoryCreated;
        /// <summary>
        /// Event for product edited.
        /// </summary>
        public event ProductEditedHandler OnProductEdited;
        /// <summary>
        /// Event for category edited.
        /// </summary>
        public event ProductCategoryEditedHandler OnProductCategoryEdited;

        /// <summary>
        /// The socket connection
        /// </summary>
        private ISocketConnection _conn;
        /// <summary>
        /// XML-Marshaller
        /// </summary>
        private IProtocolMarshal _marshal = new XmlMarshal();
        /// <summary>
        /// XML-buffer.
        /// </summary>
        private IProtocolBuffer _buffer = new XmlBuffer();


        /// <summary>
        /// CTOR. Subscribes HandleDataReceived to the OnDataReceived event.
        /// </summary>
        /// <param name="conn">The socketconnection to use</param>
        public CommandListener(ISocketConnection conn)
        {
            _conn = conn;
            _conn.OnDataRecieved += HandleDataRecieved;
        }
        /// <summary>
        /// Eventhandler for data received. For each command, it calls the HandleCommandReceived.
        /// </summary>
        /// <param name="data">The received data.</param>
        private void HandleDataRecieved(string data)
        {
            _buffer.AddData(data);

            foreach (var doc in _buffer.GetDocuments())
            {
                Debug.WriteLine("DOC: " + doc);

                var cmd = _marshal.Decode(doc);
                HandleCommandRecieved(cmd);
            }
        }
        /// <summary>
        /// Switches on the commandname, and raises the specific event for that command.
        /// </summary>
        /// <param name="cmd">The command to switch on.</param>
        private void HandleCommandRecieved(Command cmd)
        {
            OnCommandRecieved?.Invoke(cmd);

            switch (cmd.CmdName)
            {
                case "ProductCreated":
                    OnProductCreated?.Invoke((ProductCreatedCmd)cmd);
                    break;
                case "CatalogueDetails":
                    OnCatalogueDetails?.Invoke((CatalogueDetailsCmd)cmd);
                    break;
                case "ProductCategoryCreated":
                    OnProductCategoryCreated?.Invoke((ProductCategoryCreatedCmd)cmd);
                    break;
                case "ProductDeleted":
                    OnProductDeleted?.Invoke((ProductDeletedCmd)cmd);
                    break;
                case "ProductCategoryDeleted":
                    OnProductCategoryDeleted?.Invoke((ProductCategoryDeletedCmd) cmd);
                    break;
                case "ProductEdited":
                    OnProductEdited?.Invoke((ProductEditedCmd) cmd);
                    break;
                case "ProductCategoryEdited":
                    OnProductCategoryEdited?.Invoke((ProductCategoryEditedCmd)cmd);
                    break;

            }
        }
    }
}
