using System;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.UnitTest.ProtocolMarshallers
{

    [TestFixture]
    class XmlMarshalUnitTest
    {
        XmlMarshal marshal;
        GetCatalogueMarshal gcMarshal;
        GetCatalogueCmd cmd;
        wrongCmd wcmd;
        string data;


        [SetUp]
        public void SetUp()
        {
            marshal = Substitute.For<XmlMarshal>();
            gcMarshal = Substitute.For<GetCatalogueMarshal>();
            wcmd = Substitute.For<wrongCmd>();
            cmd = Substitute.For<GetCatalogueCmd>();
            data = marshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            marshal = null;
            gcMarshal = null;
            wcmd = null;
            cmd = null;
            data = null;
        }

        [Test]
        public void Encode_CommandExist_CallCmdEncode()
        {
            marshal.Encode(cmd);

            gcMarshal.Received(1).Encode(cmd);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Encode_CommandDoesntExist_ThrowException()
        {
            marshal.Encode(wcmd);
        } 

        [Test]
        public void Decode_ClassExists_CreateInstance()
        {
            marshal.Decode(data);

            gcMarshal.Received(1).Decode(data);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Decode_ClassDoesntExist_ThrowException()
        {
            string wrongData = "<Command Name = 'wrongCmd' />";

            marshal.Decode(wrongData);
        } 
    }
}
