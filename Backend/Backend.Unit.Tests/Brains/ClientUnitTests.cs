using System;
using Backend.Models.Communication;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Sockets;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class ClientUnitTests
    {
        [SetUp]
        public void Setup()
        {
            _uut = new Client();
            _sock = Substitute.For<ISocketConnection>();
            _uut.Conn = _sock;
        }

        private Client _uut;
        private ISocketConnection _sock;


        [Test]
        public void Connect_Call_ExpectCallToSocketConnection()
        {
            _uut.Connect();
            _sock.Received(1).Connect(Arg.Any<string>(),Arg.Any<int>());
        }


        [Test]
        public void Semd_Call_ExpectCallToSocketConnection()
        {
            _uut.Send("TEST");
            _sock.Received(1).Send("TEST");
        }

    }
}