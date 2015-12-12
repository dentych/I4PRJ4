using NSubstitute;
using NUnit.Framework;
using CentralServer.Sessions;
using CentralServer.Messaging;
using System;
using System.Collections.Generic;

namespace CentralServer.UnitTest
{
    [TestFixture]
    class SessionControl_UnitTest
    {
        [Test]
        public void Register_OneClient_SessionIdIsOne()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();

            var sessionId = control.Register(client);

            Assert.That(sessionId, Is.EqualTo(1));
        }

        [Test]
        public void Register_TwoClients_SessionIdIsTwo()
        {
            var control = new SessionControl();
            var client1 = Substitute.For<IMessageReceiver>();
            var client2 = Substitute.For<IMessageReceiver>();

            control.Register(client1);
            var sessionId = control.Register(client2);

            Assert.That(sessionId, Is.EqualTo(2));
        }

        [Test]
        public void Register_One_NoException()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();

            Assert.DoesNotThrow(() => control.Register(client));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Register_SameClientTwice_ThrowsException()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();

            control.Register(client);
            control.Register(client);
        }

        [Test]
        public void Unregister_KnownClient_NoException()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();
            var sessionId = control.Register(client);

            Assert.DoesNotThrow(() => control.Unregister(sessionId));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Unregister_UnknownClient_ThrowsException()
        {
            var control = new SessionControl();

            control.Unregister(1);
        }

        [Test]
        public void GetClient_KnownClient_ReturnsClient()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();
            var sessionId = control.Register(client);

            var returnedClient = control.GetClient(sessionId);

            Assert.AreSame(client, returnedClient);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void GetClient_UnknownClient_ThrowsException()
        {
            var control = new SessionControl();

            control.GetClient(1);
        }

        [Test]
        public void GetClients_AddOneClient_ReturnsOneClient()
        {
            var control = new SessionControl();
            var client = Substitute.For<IMessageReceiver>();

            control.Register(client);
            var clients = control.GetClients();

            Assert.That(clients.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetClients_AddFiveClient_ReturnsFiveClients()
        {
            var control = new SessionControl();
            var client1 = Substitute.For<IMessageReceiver>();
            var client2 = Substitute.For<IMessageReceiver>();
            var client3 = Substitute.For<IMessageReceiver>();
            var client4 = Substitute.For<IMessageReceiver>();
            var client5 = Substitute.For<IMessageReceiver>();

            control.Register(client1);
            control.Register(client2);
            control.Register(client3);
            control.Register(client4);
            control.Register(client5);
            var clients = control.GetClients();

            Assert.That(clients.Count, Is.EqualTo(5));
        }
    }
}
