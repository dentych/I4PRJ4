using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    public class Command
    {
        public string CmdName
        {
            get
            {
                // Strip class name of Cmd postfix
                var name = this.GetType().Name;
                var pos = name.IndexOf("Cmd");
                return name.Substring(0, pos);
            }
        }
    }
}
