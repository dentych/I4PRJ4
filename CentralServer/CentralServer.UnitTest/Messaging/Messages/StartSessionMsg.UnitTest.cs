using NSubstitute;
using NUnit.Framework;
using System;
using CentralServer.Sessions;
using CentralServer.Messaging;
using CentralServer.Logging;
using CentralServer.Handlers;
using CentralServer.Messaging.Messages;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;
using SharedLib.Models;

namespace CentralServer.UnitTest.Messaging
{
    [TestFixture]
    class StartSessionMsg_UnitTest
    {
        [Test]
        public void Construct_GiveClientAsParameter_RecieveSameParameter()
        {
            var client = Substitute.For<IMessageReceiver>();
            var msg = new StartSessionMsg(client);

            Assert.That(msg.Client, Is.EqualTo(client));
        }
    }
}
