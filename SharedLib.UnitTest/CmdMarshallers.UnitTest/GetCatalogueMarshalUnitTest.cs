using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest.CmdMarshallers.UnitTest
{
    [TestFixture]
    class GetCatalogueMarshalUnitTest
    {

        private GetCatalogueCmd cmd;
        private GetCatalogueMarshal gcMarshal;
        private string data;

        [SetUp]
        public void SetUp()
        {
            cmd = Substitute.For<GetCatalogueCmd>();
            gcMarshal = Substitute.For<GetCatalogueMarshal>();
            data = gcMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            cmd = null;
            gcMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = gcMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"GetCatalogue\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = gcMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }
    }
}
