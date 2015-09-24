using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    public class Protocol
    {
        private IProtocolMarshal _marshaller = new XmlMarshal();
    }
}
