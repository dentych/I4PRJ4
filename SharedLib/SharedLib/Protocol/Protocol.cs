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
        /// Call AddData on the buffer with the data received.
        /// </summary>
        /// <param name="data">recieved data</param>
        public void AddData(string data)
        {
            _buffer.AddData(data);
        }

        /// <summary>
        /// Call GetDocuments on the buffer.
        /// </summary>
        /// <returns>Enumerable with commands</returns>
        public IEnumerable<Command> GetCommands()
        {
            foreach (var doc in _buffer.GetDocuments())
                yield return _marshaller.Decode(doc.Replace("\0", ""));
        }

        /// <summary>
        /// Call the Encode function on the marshaller with the cmd parameter.
        /// </summary>
        /// <param name="cmd">Command which is to be parsed</param>
        /// <returns>Xml string with command data</returns>
        public string Encode(Command cmd)
        {
            return _marshaller.Encode(cmd);
        }

        /// <summary>
        /// Call the Decode function on the marshaller with the data parameter.
        /// </summary>
        /// <param name="data">XML string which is to be parsed</param>
        /// <returns>Command object with data from the XML string</returns>
        public Command Decode(string data)
        {
            return _marshaller.Decode(data);
        }
    }
}
