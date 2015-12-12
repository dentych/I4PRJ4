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
    class SendCommandMsg_UnitTest
    {
        [Test]
        public void Construct_GiveCommandAsParameter_RecieveSameParameter()
        {
            var cmd = new CreateProductCmd("", "", 0, 0);
            var msg = new SendCommandMsg(cmd);

            Assert.That(msg.Command, Is.EqualTo(cmd));
        }
    }
}
