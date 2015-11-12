using System.Collections.Generic;
using System.Linq;
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
}
