using System.Collections.Generic;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.Protocol
{
    public class Protocol: IProtocol
    {
        private IProtocolMarshal _marshaller = new XmlMarshal();
        private XmlBuffer _buffer = new XmlBuffer();

        public void AddData(string data)
        {
            _buffer.AddData(data);
        }

        public IEnumerable<Command> GetCommands()
        {
            foreach (var doc in _buffer.GetDocuments())
                yield return _marshaller.Decode(doc);
        }

        public string Encode(Command cmd)
        {
            return _marshaller.Encode(cmd);
        }

        public Command Decode(string data)
        {
            return _marshaller.Decode(data);
        }
    }
}
