using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace KasseApparat
{
    public class Connection : IConnection
    {
        private string Ip;
        private int Port;
        private TcpClient client = null;
        private NetworkStream stream = null;

        /// <summary>
        /// Constructor, som tager mod ip og port
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
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


        /// <summary>
        /// Funktion til at sende data på den oprettede TcpClient
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data)
        {
            if (client == null || stream == null) return;
            
            try
            {
                byte[] sendMsg = Encoding.Unicode.GetBytes(data);
                stream.Write(sendMsg, 0, sendMsg.Length);
            }
            catch (Exception)
            {
                MessageBox.Show("Error writing from socket");
                return;
            }

        }

        /// <summary>
        /// Funktion som læser fra den oprettede TcpClient
        /// </summary>
        /// <returns></returns>
        public string Receive()
        {
            if (client == null || stream == null) return null;
            
            byte[] recMsg = new byte[1024];

            try
            {               
                stream.Read(recMsg, 0, recMsg.Length); 
            }
            catch (Exception)
            {
                MessageBox.Show("Error reading from socket");
                return null;
            }

            return Encoding.Unicode.GetString(recMsg);
        }

        /// <summary>
        /// Opretter TcpClienten og henter den NetworkStream som skal skrives/læses på
        /// </summary>
        public void Connect()
        {
            client = new TcpClient(Ip, Port);
            stream = client.GetStream();
        }


        /// <summary>
        /// Lukker NetworkStream og TcpClienten 
        /// </summary>
        public void Disconnect()
        {
            stream.Close();
            client.Close();
        }
    }
}