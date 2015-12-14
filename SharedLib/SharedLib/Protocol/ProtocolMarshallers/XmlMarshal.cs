using System;
using System.IO;
using System.Resources;
using System.Xml;
using SharedLib.Protocol.CmdMarshallers;

namespace SharedLib.Protocol.ProtocolMarshallers
{
    /// <summary>
    /// Implements the IProtocolMarshaller and is responsible for finding the correct marshaller for a specific command.
    /// </summary>
    public class XmlMarshal : IProtocolMarshal
    {
        /// <summary>
        /// Adds postfix "Marshal" to command name, Searches for a classt with the specific name, if class doesnt exist, throws exception.
        /// Then creates an instance of the specific marshal class needed to encode, casts the interface ICmdMarshal to the variable to use the properties generically.
        /// </summary>
        /// <param name="cmd">Command to be parsed</param>
        /// <returns>string from the encode call from the correct marshaller</returns>
        public string Encode(Command cmd)
        {
            // Add postfix "Marshal" to command name
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

        /// <summary>
        ///  Create XmlReader to find command name, then read to the "Command" node, and save the Name attribute into a cmdName variable.
        ///  Adds a postfix "Marshal" and searches for a class with that specific name.
        ///  if class doesnt exist, throw exception.
        ///  Creates instance of the specific type of marshaller and casts ICmdMarshal interface to it.
        /// </summary>
        /// <param name="data">XML string to be parsed</param>
        /// <returns>Command object from the decode call from the correct marshaller</returns>
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
