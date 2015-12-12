using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands;

namespace SharedLib.IntegrationTest
{
    [TestFixture]
    class ProtocolToFakeMarshalIntegrationTest
    {
        private FakeCmd cmd;
        private Protocol.Protocol protocol;
        private string data;

        [SetUp]
        public void SetUp()
        {
            cmd = new FakeCmd(false);
            protocol = new Protocol.Protocol();
        }

        [TearDown]
        public void TearDown()
        {
            cmd = null;
            protocol = null;
            data = null;
        }

        [Test]
        public void Protocol_FakeMarshal_EncodeCalled_True()
        {
            protocol.Encode(cmd);

            Assert.IsTrue(cmd.EncodeIsCalled);
        }

        [Test]
        public void Protocol_FakeMarshal_EncodeCalled_False()
        {
            Assert.IsFalse(cmd.EncodeIsCalled);
        }

        [Test]
        public void Protocol_FakeMarshal_DecodeCalled_True()
        {
            data = protocol.Encode(cmd);

            var newCmd = (FakeCmd)protocol.Decode(data);

            Assert.IsTrue(newCmd.DecodeIsCalled);
        }

        [Test]
        public void Protocol_FakeMarshal_DecodeCalled_False()
        {
            Assert.IsFalse(cmd.DecodeIsCalled);
        }
    }
}
