using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol
{
    /// <summary>
    /// An abstract class which all commands inherit from to gain the CmdName attribute.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Command name used to identify command, when get is called ,postfix "Cmd" is removed before CmdName is returned.
        /// </summary>
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
