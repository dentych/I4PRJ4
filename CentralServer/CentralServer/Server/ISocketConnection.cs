using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Server
{
    public delegate void DataRecievedHandler(string data);
    public delegate void DisconnectedHandler();

    public interface ISocketConnection
    {
        event DataRecievedHandler OnDataRecieved;
        event DisconnectedHandler OnDisconnect;

        void Send(string data);
        void StartRecieving();
    }
}
