using System.Net.Sockets;
using Backend.Models.Brains;
using SharedLib.Sockets;

namespace Backend.Models.Communication
{
    /// <summary>
    /// Socket connection client.
    /// </summary>
    public class Client : IClient
    {
        private readonly SocketConnection _conn = LSC.Connection;
        private TcpClient client = null;

        public IError Error = new Error();

        public bool Connect()
        {
            LSC.Connection.Connect("127.0.0.1", 7913); // Skal bruge settings.
            return true; // Burde være void.
        }

        public bool Send(string data)
        {
            _conn.Send(data);
            return true; // same shit.
        }
    }
}