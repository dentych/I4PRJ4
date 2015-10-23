using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Backend.Brains;
using System.Collections.Generic;

namespace Backend.Communication
{
    public class Client : IClient
    {
        private TcpClient client = null;
        private string ip;
        private int port;

        public Client()
        {
            ip = "192.168.245.1";
            port = 11000;
        }
        public IError Error = new Error();

        public bool Connect()
        {
            try
            {
                client = new TcpClient(ip, port);

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                client.Close();

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public bool Send(string data)
        {
            if (client == null)
            {
                return false;
            }
            var stream = client.GetStream();

            try
            {
                var toSend = Encoding.Unicode.GetBytes(data);
                stream.Write(toSend, 0, toSend.Length);
            }

            catch (Exception)
            {
                Error.StdErr("Error in connecting to server.");
                return false;
            }

            return true;
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
    }
}