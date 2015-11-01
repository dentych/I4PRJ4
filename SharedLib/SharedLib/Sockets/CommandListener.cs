using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.Sockets
{
    public delegate void CommandRecievedHandler(Command cmd);
    public delegate void ProductCreatedHandler(ProductCreatedCmd cmd);
    public delegate void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);


    public class CommandListener
    {
        public event CommandRecievedHandler OnCommandRecieved;
        public event ProductCreatedHandler OnProductCreated;
        public event CatalogueDetailsHandler OnCatalogueDetails;

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
            }
        }
    }
}
