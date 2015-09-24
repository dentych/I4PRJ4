using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    public interface ICmdMarshal
    {
        string Encode(Command cmd);
        Command Decode(string data);
    }
}
