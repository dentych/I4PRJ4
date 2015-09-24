using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    public class XmlMarshal: IProtocolMarshal
    {
        public string Encode(Command cmd)
        {
            // 1. Ud fra cmd.CmdName, find en ICmdMarshal
            // 2. Initier ICmdMarshal
            // 3. Kald ICmdMarshal.Encode(cmd). Returner hvad den returnerer!

            return "Not yet implementet";
        }

        public Command Decode(string data)
        {
            // 1: Ud fra data, find kommando navn!
            // 2. Ud fra kommando navn fundet, find ICmdMarshal
            // 3. Initier ICmdMarshal
            // 4: Kald ICmdMarshal.Decode(data). Returner hvad den returnerer!
            return null;
        }
    }
}
