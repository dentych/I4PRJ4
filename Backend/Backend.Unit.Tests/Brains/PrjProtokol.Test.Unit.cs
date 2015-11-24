using Backend.Models.Brains;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class PrjProtocolUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _uut.LocalProtocol = Substitute.For<SharedLib.Protocol.IProtocol>();
        }

        private PrjProtokol _uut;
        private SharedLib.Protocol.IProtocol _protocol;




    }
}