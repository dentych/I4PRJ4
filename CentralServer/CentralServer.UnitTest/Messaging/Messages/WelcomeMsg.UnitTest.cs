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
    class WelcomeMsg_UnitTest
    {
        [Test]
        public void Construct_GiveSessionIdAsParameter_RecieveSameParameter()
        {
            var sessionId = 25L;
            var msg = new WelcomeMsg(sessionId);

            Assert.That(msg.SessionId, Is.EqualTo(sessionId));
        }
    }
}
