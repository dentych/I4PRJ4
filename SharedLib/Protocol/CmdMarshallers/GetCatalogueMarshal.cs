using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Protocol.Commands;

namespace SharedLib.Protocol.CmdMarshallers
{
    public class GetCatalogueMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            // Cast to GetCatalogueCmd
            var gccmd = (GetCatalogueCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", gccmd.CmdName); // "Name" attribute to root

                writer.WriteEndElement(); // Root end
            }
            // Convert stringbuilder to string and return
            return sb.ToString();
        }

        public Command Decode(string data)
        {
            // return new GetCatalogueCmd 
            return new GetCatalogueCmd();
        }
    }
}
