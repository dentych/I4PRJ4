using System;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Threading;
using System.Collections.Concurrent;

namespace SharedLib.Sockets
{
    public class SocketMsgReceiver : ThreadBase
    {
        public delegate void CommandRecievedHandler(Command cmd);
        public delegate void ProductCreatedHandler(ProductCreatedCmd cmd);
        public delegate void CatalogueDetailsHandler(CatalogueDetailsCmd cmd);

        public event CommandRecievedHandler OnCommandRecieved;
        public event ProductCreatedHandler OnProductCreated;
        public event CatalogueDetailsHandler OnCatalogueDetails;

        private SocketConnection _conn;
        private Protocol.Protocol _protocol = new Protocol.Protocol(); // FIXME: Namespace? WTF?
        private readonly BlockingCollection<string> _stringBuffer = new BlockingCollection<string>();


        public SocketMsgReceiver(SocketConnection conn)
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
                default:
                    throw new Exception("Can not handle command: " + cmd.CmdName);
            }
        }
    }
}
