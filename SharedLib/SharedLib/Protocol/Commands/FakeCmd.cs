using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// A Fake used for testing to determined if functions have been called.
    /// </summary>
    public class FakeCmd : Command
    {
        private bool _decodeIsCalled;
        
        /// <summary>
        /// Determines if the Encode function have been called.
        /// </summary>
        public bool EncodeIsCalled { get; set; }

        /// <summary>
        /// Determines if the Decode function have been called.
        /// </summary>
        public bool DecodeIsCalled { get { return _decodeIsCalled; } }

        /// <summary>
        /// Constructor for the FakeCmd, which needs the decodeCalled in the constructor to set the bool attribute _decodeIsCalled.
        /// </summary>
        /// <param name="decodeCalled">boolean value to set the encode or decode called attribute.</param>
        public FakeCmd(bool decodeCalled)
        {
            _decodeIsCalled = decodeCalled;
        }

    }
}
