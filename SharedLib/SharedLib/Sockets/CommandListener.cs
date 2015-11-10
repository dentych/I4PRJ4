using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.Sockets
{
    public delegate void CommandRecievedHandler(Command cmd);
    public delegate void ProductCreatedHandler(ProductCreatedCmd cmd);
    public delegate void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);
    public delegate void ProductCategoryCreatedHandler(ProductCategoryCreatedCmd cmd);
    public delegate void ProductDeletedHandler(ProductDeletedCmd cmd);
    public delegate void ProductCategoryDeletedHandler(ProductCategoryDeletedCmd cmd);
    public delegate void ProductCategoryEditedHandler(ProductCategoryEditedCmd cmd);
    public delegate void ProductEditedHandler(ProductEditedCmd cmd);


    public class CommandListener
    {
        public event CommandRecievedHandler OnCommandRecieved;

        public event ProductCreatedHandler OnProductCreated;
        public event CatalogueDetailsHandler OnCatalogueDetails;
        public event ProductDeletedHandler OnProductDeleted;
        public event ProductCategoryDeletedHandler OnProductCategoryDeleted;
        public event ProductCategoryCreatedHandler OnProductCategoryCreated;
        public event ProductEditedHandler OnProductEdited;
        public event ProductCategoryEditedHandler OnProductCategoryEdited;

        private ISocketConnection _conn;
        private IProtocolMarshal _marshal = new XmlMarshal();
        private IProtocolBuffer _buffer = new XmlBuffer();


        public CommandListener(ISocketConnection conn)
        {
            _conn = conn;
            _conn.OnDataRecieved += HandleDataRecieved;
        }

        private void HandleDataRecieved(string data)
        {
            _buffer.AddData(data);

            foreach (var doc in _buffer.GetDocuments())
            {
                var cmd = _marshal.Decode(doc);
                HandleCommandRecieved(cmd);
            }
        }

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
