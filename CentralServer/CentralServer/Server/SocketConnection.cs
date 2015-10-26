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
        private Socket _handle;
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];

        // Delegates
        public delegate void DataRecievedHandler(string data);
        public delegate void DisconnectedHandler();

        // Events
        public event DataRecievedHandler OnDataRecieved;
        public event DisconnectedHandler OnDisconnect;


        public SocketConnection(Log log, Socket handle)
        {
            _log = log;
            _handle = handle;
        }

        public void Send(string data)
        {
            _log.Write("SocketConnection", Log.DEBUG,
                       "Sending data (" + data.Length + " characters): " + data);
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            _handle.Send(bytes);
        }

        public void StartRecieving()
        {
            BeginAsyncRead();
        }

        private void BeginAsyncRead()
        {
            var callback = new AsyncCallback(ReadCallback);
            try
            {
                _handle.BeginReceive(_buffer, 0, _bufferSize, 0, callback, null);
            }
            catch (SocketException e)
            {
                _log.Write("SocketConnection", Log.ERROR,
                           "BeginAsyncRead error code: " + e.ErrorCode);
                throw e;
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            int bytesRead;

            try
            {
                bytesRead = _handle.EndReceive(ar);
            } catch (SocketException){
                bytesRead = 0;
            }

            if (bytesRead > 0)
            {
                HandleDataRecieved(_buffer, bytesRead);
                BeginAsyncRead();
            }
            else
            {
                _log.Write("SocketConnection", Log.DEBUG, "Read failed (0 bytes)");
                HandleConnectionClosed();
            }
        }

        private void HandleDataRecieved(byte[] data, int length)
        {
            var dataStr = Encoding.Unicode.GetString(_buffer, 0, length);
            _log.Write("SocketConnection", Log.DEBUG, "Data recieved: " + dataStr);
            OnDataRecieved?.Invoke(dataStr);
        }

        private void HandleConnectionClosed()
        {
            _log.Write("SocketConnection", Log.DEBUG, "Closing connection");
            _handle.Close();
            OnDisconnect?.Invoke();
        }
    }
}
