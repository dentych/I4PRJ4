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
        string Receive();
        void Connect();
        void Disconnect();
    }

    /*--------------------------------------------------------------------------------------------*/

    public class Connection : IConnection
    {
        public string Ip;
        public int Port;
        private TcpClient client = null;

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
            if (client == null) return;

            var stream = client.GetStream();

            try
            {
                var send = Encoding.Unicode.GetBytes(data);
                stream.Write(send, 0, send.Length);
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Error Sending");
            }
            finally
            {
                stream.Close();
            }
        }

        public string Receive()
        {
            try
            {
                var stream = client.GetStream();

                var sb = new StringBuilder();

                byte[] read;
                int actualRead;
                //do
                //{
                int size = client.ReceiveBufferSize;
                size = 1024;
                read = new byte[size];
                actualRead = stream.Read(read, 0, read.Length);

                string readToString = Encoding.Unicode.GetString(read, 0, actualRead);
                sb.Append(readToString);

                //} while (actualRead > 0);

                return sb.ToString();
            }
            catch (SocketException)
            {
                return null;
            }
        }

        public void Connect()
        {
            client = new TcpClient(Ip, Port);
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
