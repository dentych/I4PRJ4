using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Communication
{
    public interface IClient
    {
        string Ip { get; }
        int Port { get; }

        bool Send(string data);
    }
}
