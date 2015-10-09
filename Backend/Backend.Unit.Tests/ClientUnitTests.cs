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
            _uut = new Client("93.184.216.34", 80);
        }

        private Client _uut;


        [Test]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "Bad IP")]
        public void IP_SetBadIP_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {

            var uut = new Client("1222222.111.222.336.588.85.55.444", 9000);
        }

        [Test]
        public void IP_SetIp_ExpectIP()
        {
            Assert.That(_uut.Ip, Is.EqualTo("93.184.216.34"));
        }

        [Test]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "Bad port")]
        public void IP_SetPortHigh_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            _uut = new Client("93.184.216.34", 900000);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "Bad port")]
        public void IP_SetPortLow_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            _uut = new Client("93.184.216.34", -1);
        }

        [Test]
        public void Port_SetPort_ExpectPort()
        {
            Assert.That(_uut.Port, Is.EqualTo(80));
        }

        [Test]
        public void Send_RealtTest_ExpectTrue()
        {
            Assert.True(_uut.Send("TEST"));
        }

    }
}