using System.Net;
using System.Net.Sockets;
using SharedLib.Messaging;
using SharedLib.Messaging.Messages;
using System;
using System.Text;

namespace SharedLib.Sockets
{
    public class SocketConnection
    {
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];
        private readonly Socket _handle = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

        public delegate void ConnectionOpenedHandler();
        public delegate void ConnectionClosedHandler();
        public delegate void DataRecievedHandler(string data);

        public event ConnectionOpenedHandler OnConnectionOpened;
        public event ConnectionClosedHandler OnConnectionClosed;
        public event DataRecievedHandler OnDataRecieved;


        public void Connect(string ip, int port)
        {
            var address = IPAddress.Parse(ip);
            var endPoint = new IPEndPoint(address, port);
            var callback = new AsyncCallback(HandleConnected);

            _handle.BeginConnect(endPoint, callback, _handle);
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
                throw e;
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            int bytesRead;

            try
            {
                bytesRead = _handle.EndReceive(ar);
            }
            catch (SocketException)
            {
                bytesRead = 0;
            }

            if (bytesRead > 0)
            {
                BeginAsyncRead();
                HandleDataRecieved(bytesRead);
            }
            else
            {
                HandleDisconnect();
            }
        }

        public void Send(string data)
        {
            var bytes = Encoding.Unicode.GetBytes(data);
            _handle.Send(bytes);
        }

        private void HandleConnected(IAsyncResult ar)
        {
            OnConnectionClosed?.Invoke();
            BeginAsyncRead();
        }

        private void HandleDisconnect()
        {
            OnConnectionOpened?.Invoke();
        }

        private void HandleDataRecieved(int length)
        {
            var dataStr = Encoding.Unicode.GetString(_buffer, 0, length);
            OnDataRecieved?.Invoke(dataStr);
        }
    }
}
