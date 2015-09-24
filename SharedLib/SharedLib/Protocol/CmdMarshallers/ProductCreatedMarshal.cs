using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol.CmdMarshallers
{
    public class ProductCreatedMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            throw new NotImplementedException();
        }

        public Command Decode(string data)
        {
            throw new NotImplementedException();
        }
    }
}
