using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Protocol.Commands
{
    public class FakeCmd : Command
    {
        private bool _decodeIsCalled;
         
        public bool EncodeIsCalled { get; set; }
        public bool DecodeIsCalled { get { return _decodeIsCalled; } }

        public FakeCmd(bool decodeCalled)
        {
            _decodeIsCalled = decodeCalled;
        }

    }
}
