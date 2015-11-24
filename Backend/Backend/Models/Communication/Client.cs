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
        public ISocketConnection Conn = LSC.Connection;

        public bool Connect()
        {
            Conn.Connect("127.0.0.1", 7913); // Skal bruge settings.
            return true; // Burde være void.
        }

        public bool Send(string data)
        {
            Conn.Send(data);
            return true; // same shit.
        }
    }
}