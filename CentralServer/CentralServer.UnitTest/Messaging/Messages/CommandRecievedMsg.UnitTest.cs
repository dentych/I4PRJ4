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
    class CommandRecievedMsg_UnitTest
    {
        [Test]
        public void Construct_GiveCommandAsParameter_RecieveSameParameter()
        {
            var sessionId = 192L;
            var cmd = new CreateProductCmd("", "", 0, 0);
            var msg = new CommandRecievedMsg(sessionId, cmd);

            Assert.That(msg.Command, Is.EqualTo(cmd));
        }

        [Test]
        public void Construct_GiveSessionIdAsParameter_RecieveSameParameter()
        {
            var sessionId = 192L;
            var cmd = new CreateProductCmd("", "", 0, 0);
            var msg = new CommandRecievedMsg(sessionId, cmd);

            Assert.That(msg.SessionId, Is.EqualTo(sessionId));
        }
    }
}
