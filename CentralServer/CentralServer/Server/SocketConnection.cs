using System;
using System.Net.Sockets;
using System.Text;
using CentralServer.Logging;
using CentralServer.Messaging.Messages;

namespace CentralServer.Server
{
    class SocketConnection
    {
        private Log _log;
        private ClientControl _client;
        private Socket _handle;
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];


        public SocketConnection(Log log, Socket handle, ClientControl client)
        {
            _handle = handle;
            _client = client;
        }

        public void Send(string data)
        {
            _log.Write(this, "Sending data (" + data.Length + " characters)");
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
                _log.Write(this, "Data recieved");

                var data = Encoding.Unicode.GetString(_buffer, 0, bytesRead);
                var msg = new DataRecievedMsg(data);
                _client.Send(ClientControl.E_DATA_RECIEVED, msg);

                BeginAsyncRead();
            }
            else
            {
                _log.Write(this, "Read failed (0 bytes)");
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
