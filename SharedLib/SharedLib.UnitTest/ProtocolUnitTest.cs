using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.ProtocolMarshallers;

namespace SharedLib.UnitTest
{   [TestFixture]
    public class ProtocolUnitTest
    {
        XmlMarshal marshal;
        XmlBuffer buffer;
        Protocol.Protocol protocol;
        GetCatalogueCmd cmd;
        string data;
    
    [SetUp]
    public void Setup()
    {
        marshal = Substitute.For<XmlMarshal>();
        buffer = Substitute.For<XmlBuffer>();
        cmd = Substitute.For<GetCatalogueCmd>();
        protocol = new Protocol.Protocol();
        data = protocol.Encode(cmd);

    }

    [TearDown]
    public void TearDown()
    {
        marshal = null;
        buffer = null;
        cmd = null;
        protocol = null;
        data = null;
    }
    
    [Test]
    public void Encode_MarshalEncodeCalled()
    {
        protocol.Encode(cmd);

        marshal.Received(1).Encode(cmd);
    }

    [Test]
    public void Decode_MarshalEncodeCalled()
    {
        protocol.Decode(data);

        marshal.Received(1).Decode(data);
    }

    [Test]
    public void AddData_BufferAddCalled()
    {
        protocol.AddData(data);

        buffer.Received(1).AddData(data);
    }

    ///// Vil du implementere denne jakob???? :D
    /*
    [Test]
    public void GetCommands()
    {
        
    }
    */
    }
}
