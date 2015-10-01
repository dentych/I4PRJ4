using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Backend.Communication
{
    public class Client : IClient
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }

        public Client(string ip, int port)
        {
            if (port < 1 || port > 65535)
            {
                throw new ArgumentException("Bad port");
            }
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address))
            {
                throw new ArgumentException("Bad IP");
            }

            Ip = ip;
            Port = port;
        }

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

            catch(Exception e)
            {
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
            catch (System.Net.Sockets.SocketException)
            {
                return null;
            }
           
        }
    }
}
