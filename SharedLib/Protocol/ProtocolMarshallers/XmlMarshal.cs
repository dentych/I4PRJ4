using System;
using System.IO;
using System.Resources;
using System.Xml;
using SharedLib.Protocol.CmdMarshallers;

namespace SharedLib.Protocol.ProtocolMarshallers
{
    public class XmlMarshal : IProtocolMarshal
    {
        public string Encode(Command cmd)
        {
            // Add postfix "Cmd" to command name
            string fullname = cmd.CmdName.ToString() + "Marshal";

            // Searches for a class with the specific name
            Type mytype = Type.GetType("SharedLib.Protocol.CmdMarshallers." + fullname);

            // if class doesnt exist, throws exception
            if (mytype == null)
            {
                throw new Exception("Command " + fullname + " not found");
            }

            // Creates an instance of the specific marshal class needed to encode
            var temp = Activator.CreateInstance(mytype);

            // Casts the interface of the marshals to the variable to use the properties generically
            var cmdtype = (ICmdMarshal)temp;

            // return the encoded xml string from the specific instance
            return cmdtype.Encode(cmd);
        }

        public Command Decode(string data)
        {
            string cmdName;

            // Create XmlReader to find commandName
            using (XmlReader reader = XmlReader.Create(new StringReader(data)))
            {
                reader.ReadToFollowing("Command"); // Read from <Command> node (root in this case)
                cmdName = reader["Name"]; // Sets the attribute name from Command into cmdName
            }
            // Adds postfix "Marshal" to commandName
            cmdName = cmdName + "Marshal";

            // Searches for a class with the specific name
            Type mytype = Type.GetType("SharedLib.Protocol.CmdMarshallers." + cmdName);

            // if class doesnt exist, throws exception
            if (mytype == null)
            {
                throw new Exception("Command " + cmdName + " not found");
            }

            // Creates an instance of the specific marshal class needed to encode
            var temp = Activator.CreateInstance(mytype);

            // Casts the interface of the marshals to the variable to use the properties generically
            var cmdtype = (ICmdMarshal)temp;

            // Return the decoded ICmd from the xml string
            return cmdtype.Decode(data);
        }
    }
}
