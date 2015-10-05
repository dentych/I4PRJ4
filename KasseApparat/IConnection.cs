using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    public interface IConnection
    {
        bool Connect();
        bool DisConnect();
        void Send();
        void Receive();
    }

    public class Connection //: IConnection
    {

    
    }
}
