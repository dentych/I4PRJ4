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
    class DataRecievedMsg_UnitTest
    {
        [Test]
        public void Construct_GiveDataAsParameter_RecieveSameParameter()
        {
            var s = "hejsa!";
            var msg = new DataRecievedMsg(s);

            Assert.That(msg.Data, Is.EqualTo(s));
        }
    }
}
