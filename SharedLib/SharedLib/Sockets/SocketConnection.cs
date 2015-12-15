using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SharedLib.Sockets
{
    public delegate void ConnectionOpenedHandler();
    public delegate void ConnectionClosedHandler();
    public delegate void ConnectionErrorHandler(SocketException err);
    public delegate void DataRecievedHandler(string data);

    /// <summary>
    /// An interface for create an asynchronous socket client
    /// </summary>
    public interface ISocketConnection
    {
        event ConnectionOpenedHandler OnConnectionOpened;
        event ConnectionClosedHandler OnConnectionClosed;
        event ConnectionErrorHandler OnConnectionError;
        event DataRecievedHandler OnDataRecieved;
        void Connect(string ip, int port);
        void Send(string data);
    }

    /// <summary>
    /// Implements an asynchronous socket client.
    /// </summary>
    public class SocketConnection : ISocketConnection
    {
        private readonly Socket _handle;
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];

        public event ConnectionOpenedHandler OnConnectionOpened;
        public event ConnectionClosedHandler OnConnectionClosed;
        public event ConnectionErrorHandler OnConnectionError;
        public event DataRecievedHandler OnDataRecieved;


        public SocketConnection(Socket handle = null)
        {
            if (handle != null)
                _handle = handle;
            else
                _handle = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="ip">IP to connect to</param>
        /// <param name="port">Port to connect to</param>
        public void Connect(string ip, int port)
        {
            var address = IPAddress.Parse(ip);
            var endPoint = new IPEndPoint(address, port);
            var callback = new AsyncCallback(HandleConnected);

            _handle.BeginConnect(endPoint, callback, _handle);
        }

        /// <summary>
        /// Invoked when a connection has been established
        /// </summary>
        private void HandleConnected(IAsyncResult ar)
        {
            OnConnectionOpened?.Invoke();
            BeginAsyncRead();
        }

        /// <summary>
        /// Invoked when the connection has been closed.
        /// </summary>
        private void HandleDisconnect()
        {
            OnConnectionClosed?.Invoke();
        }

        /// <summary>
        /// Invoked when data has been read from the server.
        /// </summary>
        /// <param name="length">Amount of bytes read</param>
        private void HandleDataRecieved(int length)
        {
            // FIXME: Handle decoding errors
            var dataStr = Encoding.Unicode.GetString(_buffer, 0, length);

            Debug.WriteLine("Received: " + dataStr);

            OnDataRecieved?.Invoke(dataStr);
        }

        /// <summary>
        /// Send data to the server
        /// </summary>
        /// <param name="data">Raw data string</param>
        public void Send(string data)
        {
            // FIXME: Handle encoding errors
            var bytes = Encoding.Unicode.GetBytes(data);
            // FIXME: Handle write errors
            _handle.Send(bytes);
        }

        /// <summary>
        /// Begin asynchronous reading from the server
        /// </summary>
        private void BeginAsyncRead()
        {
            var callback = new AsyncCallback(ReadCallback);
            try
            {
                _handle.BeginReceive(_buffer, 0, _bufferSize, 0, callback, null);
            }
            catch (SocketException e)
            {
                OnConnectionError?.Invoke(e);
            }
        }

        /// <summary>
        /// Invoked when an asynchronous reading has completed
        /// </summary>
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
