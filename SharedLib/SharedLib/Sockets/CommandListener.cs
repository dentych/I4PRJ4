using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Threading;
using System.Collections.Concurrent;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.Sockets
{
    public delegate void CommandRecievedHandler(Command cmd);
    public delegate void ProductCreatedHandler(ProductCreatedCmd cmd);
    public delegate void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);


    public class CommandListener : ThreadBase
    {
        public event CommandRecievedHandler OnCommandRecieved;
        public event ProductCreatedHandler OnProductCreated;
        public event CatalogueDetailsHandler OnCatalogueDetails;

        private ISocketConnection _conn;
        private IProtocolMarshal _marshal = new XmlMarshal();
        private IProtocolBuffer _buffer = new XmlBuffer();

        // Enables asyncronous socket reading.
        // Is used to implement cross-thread producer-consumer problem.
        private readonly BlockingCollection<string> _readBuffer = new BlockingCollection<string>();


        public CommandListener(ISocketConnection conn)
        {
            _conn = conn;
            _conn.OnDataRecieved += HandleDataRecieved;
        }

        protected override void Run()
        {
            while (true)
            {
                var s = _readBuffer.Take(); // Blocking when empty

                _buffer.AddData(s);

                foreach (var doc in _buffer.GetDocuments())
                {
                    var cmd = _marshal.Decode(doc);
                    HandleCommandRecieved(cmd);
                }
            }
        }

        private void HandleDataRecieved(string data)
        {
            _readBuffer.Add(data);
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
