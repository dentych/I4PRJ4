using System;
using Backend.Communication;
using NUnit.Framework;

namespace Backend.Unit.Tests
{
    [TestFixture]
    public class ClientUnitTests
    {
        [SetUp]
        public void Setup()
        {
            _uut = new Client();
        }

        private Client _uut;


        [Test]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "Bad IP")]
        public void IP_SetBadIP_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {

            var uut = new Client();
        }

        [Test]
        public void Send_RealtTest_ExpectTrue()
        {
            Assert.True(_uut.Send("TEST"));
        }

    }
}