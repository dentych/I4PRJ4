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
            Conn.Connect(Properties.Settings.Default.CSIP, Properties.Settings.Default.CSPort); // Skal bruge settings.
            return true; // Burde være void.
        }

        public bool Send(string data)
        {
            Conn.Send(data);
            return true; // same shit.
        }
    }
}