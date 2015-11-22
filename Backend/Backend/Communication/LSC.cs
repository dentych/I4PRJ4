using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Sockets;

namespace Backend.Communication
{
    /// <summary>
    /// Singleton for the socket connection.
    /// </summary>
    public class LSC
    {
        private static SocketConnection _connection;
        private static CommandListener _listener;

        /// <summary>
        /// Socket connection.
        /// </summary>
        public static SocketConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SocketConnection();
                }
                return _connection;
            }
        }

        /// <summary>
        /// Socket connection command listener.
        /// </summary>
        public static CommandListener Listener
        {
            get
            {
                if (_listener == null)
                {
                    if (_connection == null)
                    {
                        _connection = new SocketConnection();
                    }

                    _listener = new CommandListener(_connection);
                }
                return _listener;
            }
        }
    }
}
