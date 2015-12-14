using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    /// <summary>
    /// Interface for the protocol buffer.
    /// </summary>
    interface IProtocolBuffer
    {
        void AddData(string data);
        IEnumerable<string> GetDocuments();
    }
}
