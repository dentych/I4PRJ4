using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    public interface IProtocol
    {
        void AddData(string data);
        IEnumerable<Command> GetCommands();
        string Encode(Command cmd);
        Command Decode(string data);
    }
}
