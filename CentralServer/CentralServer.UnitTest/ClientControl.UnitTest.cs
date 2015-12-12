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
    class ClientControl_UnitTest
    {
        ILog log;
        IMessageReceiver main;
        ISocketConnection conn;
        IProtocol protocol;
        ClientControl uut;


        [SetUp]
        public void Setup()
        {
            log = Substitute.For<ILog>();
            main = Substitute.For<IMessageReceiver>();
            conn = Substitute.For<ISocketConnection>();
            protocol = Substitute.For<IProtocol>();
            uut = new ClientControl(log, main, conn, protocol);
        }

        [Test]
        public void Dispatch_SendWelcome_StartReceivingData()
        {
            var msg = new WelcomeMsg(25L);

            uut.Dispatch(ClientControl.E_WELCOME, msg);

            conn.Received(1).StartRecieving();
        }

        [Test]
        public void Dispatch_SendCommand_StringDataIsSentToConnection()
        {
            var cmd = new CreateProductCmd("", "", 0, 0);
            var msg = new SendCommandMsg(cmd);

            uut.Dispatch(ClientControl.E_SEND_COMMAND, msg);

            conn.Received(1).Send(Arg.Any<string>());
        }


        [Test]
        public void HandleDataRecieved_TriggerInvoke_DataAddedToProtocol()
        {
            var data = "Testdata!";

            conn.OnDataRecieved += Raise.Event<DataRecievedHandler>(data);

            protocol.Received(1).AddData(data);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(9999)]
        public void HandleDataRecieved_ProtocolReturnsNCommands_MainRecievedNMessage(int cmdCount)
        {
            var list = new List<Command>();

            for (var i = 0; i < cmdCount; i++)
                list.Add(new CreateProductCmd("", "", 0, 0));

            protocol.GetCommands().Returns(list); ;
            conn.OnDataRecieved += Raise.Event<DataRecievedHandler>("");

            main.Received(cmdCount).Send(MainControl.E_COMMAND_RECEIVED, Arg.Any<CommandRecievedMsg>());
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void HandleDataRecieved_ProtocolThrowsException_ThrowsException()
        {
            protocol.GetCommands().Returns(x => { throw new Exception(); });
            conn.OnDataRecieved += Raise.Event<DataRecievedHandler>("");
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void HandleDataRecieved_ProtocolThrowsException_WriteErrorToLog()
        {
            protocol.GetCommands().Returns(x => { throw new Exception(); });
            conn.OnDataRecieved += Raise.Event<DataRecievedHandler>("");

            log.Received().Write(Arg.Any<string>(), Log.ERROR, Arg.Any<string>());
        }
    }
}
