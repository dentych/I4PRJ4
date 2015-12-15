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
    /// <summary>
    /// Marshaller used for testing. Implements the ICmdMarshal interface.
    /// </summary>
    public class FakeMarshal: ICmdMarshal
    {
        /// <summary>
        /// Used to test if the Encode command is called.
        /// </summary>
        /// <param name="cmd">Command which is to be parsed.</param>
        /// <returns>XML string</returns>
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

        /// <summary>
        /// Used to test if the Decode command is called.
        /// </summary>
        /// <param name="data">Xml string that is to be parsed.</param>
        /// <returns>FakeCmd object.</returns>
        public Command Decode(string data)
        {
            var fkcmd = new FakeCmd(true);

            return fkcmd;
        }
    }
}
