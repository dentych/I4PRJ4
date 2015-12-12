using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using CentralServer.Messaging;
using CentralServer.Messaging.Messages;
using CentralServer.Logging;
using CentralServer.Server;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace CentralServer.UnitTest
{
    [TestFixture]
    class Log_UnitTest
    {
        ILogger logger;


        [SetUp]
        public void Setup()
        {
            logger = Substitute.For<ILogger>();
        }

        [Test]
        public void Write_UseLowestLevel_IncludeAllLevels()
        {
            var uut = new Log(logger, Log.DEBUG);

            uut.Write("", Log.DEBUG, "");
            uut.Write("", Log.NOTICE, "");
            uut.Write("", Log.WARNING, "");
            uut.Write("", Log.ERROR, "");

            logger.Received(4).Write(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Write_UseHighLevel_ExcludeLowerLevels()
        {
            var uut = new Log(logger, Log.WARNING);

            uut.Write("", Log.DEBUG, "");
            uut.Write("", Log.NOTICE, "");
            uut.Write("", Log.WARNING, "");
            uut.Write("", Log.ERROR, "");

            logger.Received(2).Write(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
