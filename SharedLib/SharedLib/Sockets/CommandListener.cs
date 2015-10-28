using System;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Threading;
using System.Collections.Concurrent;

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
        private Protocol.Protocol _protocol = new Protocol.Protocol(); // FIXME: Namespace? WTF?
        private readonly BlockingCollection<string> _stringBuffer = new BlockingCollection<string>();


        public CommandListener(ISocketConnection conn)
        {
            _conn = conn;
            _conn.OnDataRecieved += HandleDataRecieved;
        }

        protected override void Run()
        {
            while (true)
            {
                var s = _stringBuffer.Take(); // Blocking when empty
                _protocol.AddData(s);

                foreach (var cmd in _protocol.GetCommands())
                    HandleCommandRecieved(cmd);
            }
        }

        private void HandleDataRecieved(string data)
        {
            _stringBuffer.Add(data);
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
