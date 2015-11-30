using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharedLib.Models;
using SharedLib.Protocol.Commands;

namespace SharedLib.Protocol.CmdMarshallers
{
    public class FakeMarshal: ICmdMarshal
    {
        public string Encode(Command cmd)
        {
            var fkcmd = (FakeCmd)cmd;

            // Create XML
            var sb = new StringBuilder(); // Create stringbuilder to collect xml data
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartElement("Command"); // Root
                writer.WriteAttributeString("Name", fkcmd.CmdName); // "Name" attribute to root

                writer.WriteEndElement(); // Root end
            }

            fkcmd.EncodeIsCalled = true;

            // Convert stringbuilder to string and return
           return sb.ToString();
        }

        public Command Decode(string data)
        {
            var fkcmd = new FakeCmd(true);

            return fkcmd;
        }
    }
}
