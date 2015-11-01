using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    interface IProtocolBuffer
    {
        void AddData(string data);
        IEnumerable<string> GetDocuments();
    }
}
