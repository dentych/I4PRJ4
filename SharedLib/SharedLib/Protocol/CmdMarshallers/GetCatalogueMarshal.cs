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
    /// <summary>
    /// Marshaller for the GetCatalogueCmd, Implements the ICmdMarshal interface.
    /// </summary>
    public class GetCatalogueMarshal: ICmdMarshal
    {
        /// <summary>
        /// Casts GetCatalogueCmd to the parameter cmd, then creates an XML string with the cmd name.
        /// </summary>
        /// <param name="cmd">Command which is to be parse, in this instance a GetCatalogueCmd</param>
        /// <returns>XML string</returns>
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

        /// <summary>
        /// Returns a GetCatalogueCmd
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>GetCatalogueCmd object</returns>
        public Command Decode(string data)
        {
            // return new GetCatalogueCmd 
            return new GetCatalogueCmd();
        }
    }
}
