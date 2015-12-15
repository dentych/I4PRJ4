using System;
using System.Net.Sockets;
using System.Text;
using CentralServer.Logging;
using CentralServer.Messaging.Messages;

namespace CentralServer.Server
{
    /// <summary>
    /// Enables asyncronous reading to a socket connection.
    /// </summary>
    public class SocketConnection : ISocketConnection
    {
        private ILog _log;
        private Socket _handle;
        private const int _bufferSize = 512;
        private byte[] _buffer = new byte[_bufferSize];

        // Events
        public event DataRecievedHandler OnDataRecieved;
        public event DisconnectedHandler OnDisconnect;


        public SocketConnection(ILog log, Socket handle)
        {
            _log = log;
            _handle = handle;
        }

        /// <summary>
        /// Send data to the socket
        /// </summary>
        /// <param name="data">Raw data</param>
        public void Send(string data)
        {
            _log.Write("SocketConnection", Log.DEBUG,
                       "Sending data (" + data.Length + " characters): " + data);
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            _handle.Send(bytes);
        }

        /// <summary>
        /// Start receiving data from the socket
        /// </summary>
        public void StartRecieving()
        {
            BeginAsyncRead();
        }

        /// <summary>
        /// Begin an asyncronous reading from the socket
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
                _log.Write("SocketConnection", Log.ERROR,
                           "BeginAsyncRead error code: " + e.ErrorCode);
                throw e;
            }
        }

        /// <summary>
        /// Invoked when an asyncronous reading has completed
        /// </summary>
        /// <param name="ar"></param>
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

        /// <summary>
        /// Invoked when new data has been received
        /// </summary>
        /// <param name="data">Raw data array</param>
        /// <param name="length">Amount of bytes read</param>
        private void HandleDataRecieved(byte[] data, int length)
        {
            var dataStr = Encoding.Unicode.GetString(_buffer, 0, length);
            _log.Write("SocketConnection", Log.DEBUG, "Data recieved: " + dataStr);
            OnDataRecieved?.Invoke(dataStr);
        }

        /// <summary>
        /// Invoked when client closed the connection
        /// </summary>
        private void HandleConnectionClosed()
        {
            _log.Write("SocketConnection", Log.DEBUG, "Closing connection");
            _handle.Close();
            OnDisconnect?.Invoke();
        }
    }
}
