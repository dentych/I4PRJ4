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
            client = new TcpClient(Ip, Port);
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
            if (client == null) return "";

            string returndata;
            var stream = client.GetStream();

            try
            {
                byte[] bytes = new byte[client.ReceiveBufferSize];
                stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
                returndata = Encoding.Unicode.GetString(bytes);
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Error Receiving");
            }
            finally
            {
                stream.Close();
            }
            return returndata;
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
