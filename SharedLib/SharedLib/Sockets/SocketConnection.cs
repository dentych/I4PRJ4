using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharedLib.Sockets
{
    public delegate void ConnectionOpenedHandler();
    public delegate void ConnectionClosedHandler();
    public delegate void DataRecievedHandler(string data);


    public interface ISocketConnection
    {
        event ConnectionOpenedHandler OnConnectionOpened;
        event ConnectionClosedHandler OnConnectionClosed;
        event DataRecievedHandler OnDataRecieved;
        void Connect(string ip, int port);
        void Send(string data);
    }


    public class SocketConnection : ISocketConnection
    {
        private readonly Socket _handle;
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];

        public event ConnectionOpenedHandler OnConnectionOpened;
        public event ConnectionClosedHandler OnConnectionClosed;
        public event DataRecievedHandler OnDataRecieved;


        public SocketConnection(Socket handle = null)
        {
            if (handle != null)
                _handle = handle;
            else
                _handle = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            var address = IPAddress.Parse(ip);
            var endPoint = new IPEndPoint(address, port);
            var callback = new AsyncCallback(HandleConnected);

            _handle.BeginConnect(endPoint, callback, _handle);
        }

        private void HandleConnected(IAsyncResult ar)
        {
            OnConnectionOpened?.Invoke();
            BeginAsyncRead();
        }

        private void HandleDisconnect()
        {
            OnConnectionClosed?.Invoke();
        }

        private void HandleDataRecieved(int length)
        {
            // FIXME: Handle decoding errors
            var dataStr = Encoding.Unicode.GetString(_buffer, 0, length);
            OnDataRecieved?.Invoke(dataStr);
        }

        public void Send(string data)
        {
            // FIXME: Handle encoding errors
            var bytes = Encoding.Unicode.GetBytes(data);
            // FIXME: Handle write errors
            _handle.Send(bytes);
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
                HandleDataRecieved(bytesRead);
                BeginAsyncRead();
            }
            else
            {
                HandleDisconnect();
            }
        }
    }
}
