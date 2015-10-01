using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Backend.Communication
{
    public class Client : IClient
    {
        public Client(string ip, int port)
        {
            if (port < 1 || port > 65535)
            {
                throw new ArgumentException("Bad port");
            }
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address))
            {
                Error.StdErr("Bad IP");
                throw new ArgumentException("Bad IP");
            }

            Ip = ip;
            Port = port;
        }

        public string Ip { get; }
        public int Port { get; }

        public bool Send(string data)
        {
            var client = Connect();
            if (client == null)
            {
                return false;
            }
            var stream = client.GetStream();

            try
            {
                var toSend = Encoding.ASCII.GetBytes(data);
                stream.Write(toSend, 0, toSend.Length);
            }

            catch (Exception e)
            {
                Error.StdErr("Error in connecting to server.");
                return false;
            }
            finally
            {
                stream.Close();
                client.Close();
            }

            return true;
        }

        private TcpClient Connect()
        {
            try
            {
                var client = new TcpClient(Ip, Port);

                return client;
            }
            catch (SocketException)
            {
                return null;
            }
        }
    }
}