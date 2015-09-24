using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Protocol.Commands;

namespace SharedLib.Protocol.CmdMarshallers
{
    public class CreateProductMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            CreateProductCmd ccmd = (CreateProductCmd)cmd;
            // implementer xml gejl
            /*

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteStartElement("Product");

                writer.WriteElementString("Name",ccmd.Name);

                writer.WriteEndElement();
                writer.WriteEndElement(); //Slutter Root
            }
            return sb.ToString();
            */
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
