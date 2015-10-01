using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Backend.Communication
{
    public class Client : IClient
    {
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public TcpClient client;

        public Client(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public bool Send(string data)
        {
            client = Connect();
            var stream = client.GetStream();

            try {
                var toSend = Encoding.ASCII.GetBytes(data);

                stream.Write(toSend, 0, toSend.Length);
            }
            catch(Exception e)
            {
                // Lel. Later we maek error page
                Console.WriteLine(e.Message);
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
            return new TcpClient(Ip, Port);
        }
    }
}
