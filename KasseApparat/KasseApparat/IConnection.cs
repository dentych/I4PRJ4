using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    public interface IConnection
    {
        void Send(string data);
        void Receive();
    }

    /*--------------------------------------------------------------------------------------------*/

    public class Connection : IConnection
    {
        public string Ip;
        public int Port;

        public Connection(string ip, int port)
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

        public void Send(string data)
        {
            var client = Connect();
            if (client == null) return;

            var stream = client.GetStream();

            try
            {
                var send = Encoding.ASCII.GetBytes(data);
                stream.Write(send, 0, send.Length);
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Error Sending");
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        public void Receive()
        {
            var client = Connect();
            if (client == null) return;

            var stream = client.GetStream();

            try
            {

            }
            catch (Exception)
            {
                throw new System.ArgumentException("Error Receiving");
            }
            finally
            {
                stream.Close();
                client.Close();
            }
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
