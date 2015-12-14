using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    /// <summary>
    /// Interface all marshallers inherit from.
    /// </summary>
    public interface ICmdMarshal
    {
        /// <summary>
        /// Parse a command object into an XML string
        /// </summary>
        /// <param name="cmd">Command which is to be parsed</param>
        /// <returns>Xml string with command data</returns>
        string Encode(Command cmd);

        /// <summary>
        /// Parse XML string into specific command object
        /// </summary>
        /// <param name="data">XML string which is to be parsed</param>
        /// <returns>Command object with data from the XML string</returns>
        Command Decode(string data);
    }
}
