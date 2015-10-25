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
        private string Ip;
        private int Port;
        private TcpClient client = null;
        private NetworkStream stream = null;

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
            
            try
            {
                var send = Encoding.Unicode.GetBytes(data);
                stream.Write(send, 0, send.Length);
            }
            catch (Exception)
            {
                return;
            }

        }

        public string Receive()
        {
            if (client == null) return null;
            
            try
            {
                var sb = new StringBuilder();
                int actualRead;
                byte[] read = new byte[client.ReceiveBufferSize];
                
                actualRead = stream.Read(read, 0, read.Length);

                string readToString = Encoding.Unicode.GetString(read, 0, actualRead);
                sb.Append(readToString);

                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public void Connect()
        {
            client = new TcpClient(Ip, Port);
            stream = client.GetStream();
        }

        public void Disconnect()
        {
            stream.Close();
            client.Close();
        }
    }
}
