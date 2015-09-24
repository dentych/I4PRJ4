using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Protocol.Commands;

namespace SharedLib.Protocol.CmdMarshallers
{
    public class CreateProductMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            cmd = (CreateProductCmd) cmd;
            // implementer xml gejl
            throw new NotImplementedException();
        }

        public Command Decode(string data)
        {
            // implementer xml tryl

            //return new CreateProductCmd();
            throw new NotImplementedException();

        }
    }
}
