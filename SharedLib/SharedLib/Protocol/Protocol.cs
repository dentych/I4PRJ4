using System.Collections.Generic;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.Protocol
{
    /// <summary>
    /// Main class which implement the IProtocol interface and uses a buffer and a ProtocolMarshaller.
    /// </summary>
    public class Protocol: IProtocol
    {
        private IProtocolMarshal _marshaller = new XmlMarshal();
        private XmlBuffer _buffer = new XmlBuffer();


        /// <summary>
        /// Add the incoming data from a transmission into a buffer collection
        /// </summary>
        /// <param name="data">recieved data</param>
        public void AddData(string data)
        {
            _buffer.AddData(data);
        }

        /// <summary>
        /// Get all the fully transferred commands from the buffer collection 
        /// </summary>
        /// <returns>Enumerable with commands</returns>
        public IEnumerable<Command> GetCommands()
        {
            foreach (var doc in _buffer.GetDocuments())
                yield return _marshaller.Decode(doc.Replace("\0", ""));
        }

        /// <summary>
        /// Parse a command object into an XML string
        /// </summary>
        /// <param name="cmd">Command which is to be parsed</param>
        /// <returns>Xml string with command data</returns>
        public string Encode(Command cmd)
        {
            return _marshaller.Encode(cmd);
        }

        /// <summary>
        /// Parse XML string into specific command object
        /// </summary>
        /// <param name="data">XML string which is to be parsed</param>
        /// <returns>Command object with data from the XML string</returns>
        public Command Decode(string data)
        {
            return _marshaller.Decode(data);
        }
    }
}
