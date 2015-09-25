using CentralServer.Messaging.Messages;
using System;
using System.Net.Sockets;
using System.Text;

namespace CentralServer.Server
{
    class SocketConnection
    {
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];
        private ClientControl _client;
        private Socket _handle;


        public SocketConnection(Socket handle, ClientControl client)
        {
            _handle = handle;
            _client = client;
        }

        public void Send(string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
        }

        public void StartRecieving()
        {
            BeginAsyncRead();
        }

        private void BeginAsyncRead()
        {
            var callback = new AsyncCallback(ReadCallback);
            _handle.BeginReceive(_buffer, 0, _bufferSize, 0, callback, null);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            int bytesRead = _handle.EndReceive(ar);

            if (bytesRead > 0)
            {
                var data = Encoding.Unicode.GetString(_buffer, 0, bytesRead);
                var msg = new DataRecievedMsg(data);
                _client.Send(ClientControl.E_DATA_RECIEVED, msg);

                BeginAsyncRead();
            }
            else
            {
                HandleConnectionClosed();
            }
        }

        private void HandleConnectionClosed()
        {
            _handle.Close();
            _client.Send(ClientControl.E_CONNECTION_CLOSED);
        }
    }
}
