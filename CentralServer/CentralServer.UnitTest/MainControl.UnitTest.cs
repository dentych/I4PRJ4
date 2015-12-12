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

namespace CentralServer.UnitTest
{
    [TestFixture]
    class MainControl_UnitTest
    {
        ILog log;
        ISessionControl sessions;
        ICommandHandler handler;
        IMessageReceiver client;
        MainControl uut;


        [SetUp]
        public void Setup()
        {
            log = Substitute.For<ILog>();
            sessions = Substitute.For<ISessionControl>();
            handler = Substitute.For<ICommandHandler>();
            client = Substitute.For<IMessageReceiver>();
            uut = new MainControl(log, sessions, handler);
        }

        [Test]
        public void Dispatch_SendStartSession_SessionRegistered()
        {
            var msg = new StartSessionMsg(client);

            uut.Dispatch(MainControl.E_START_SESSION, msg);

            sessions.Received(1).Register(client);
        }

        [Test]
        public void Dispatch_SendStartSession_ReceiveWelcomeMsgInReturn()
        {
            var msg = new StartSessionMsg(client);

            uut.Dispatch(MainControl.E_START_SESSION, msg);

            client.Received(1).Send(ClientControl.E_WELCOME, Arg.Any<WelcomeMsg>());
        }

        [Test]
        public void Dispatch_SendStopSession_SessionUnregistered()
        {
            var sessionId = 1L;
            var msg = new StopSessionMsg(sessionId);

            uut.Dispatch(MainControl.E_STOP_SESSION, msg);

            sessions.Received(1).Unregister(sessionId);
        }


        [Test]
        public void Dispatch_SendGetCatalogueCommand_HandleGetCatalogueInvoked()
        {
            var cmd = new GetCatalogueCmd();
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleGetCatalogue(Arg.Any<IMessageReceiver>(), Arg.Any<GetCatalogueCmd>());
        }

        [Test]
        public void Dispatch_SendCreateProductCommand_HandleCreateProductInvoked()
        {
            var cmd = new CreateProductCmd("", "", 0, 0);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleCreateProduct(Arg.Any<IMessageReceiver>(), Arg.Any<CreateProductCmd>());
        }

        [Test]
        public void Dispatch_SendEditProductCommand_HandleEditProductInvoked()
        {
            var cmd = new EditProductCmd("", "", 0, 0, 0);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleEditProduct(Arg.Any<IMessageReceiver>(), Arg.Any<EditProductCmd>());
        }

        [Test]
        public void Dispatch_SendDeleteProductCommand_HandleDeleteProductInvoked()
        {
            var cmd = new DeleteProductCmd(0);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleDeleteProduct(Arg.Any<IMessageReceiver>(), Arg.Any<DeleteProductCmd>());
        }

        [Test]
        public void Dispatch_SendCreateProductCategoryCommand_HandleCreateProductCategoryInvoked()
        {
            var cat = Substitute.For<ProductCategory>();
            var cmd = new CreateProductCategoryCmd(cat);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleCreateProductCategory(Arg.Any<IMessageReceiver>(), Arg.Any<CreateProductCategoryCmd>());
        }

        [Test]
        public void Dispatch_SendEditProductCategoryCommand_HandleEditProductCategoryInvoked()
        {
            var cat = Substitute.For<ProductCategory>();
            var cmd = new EditProductCategoryCmd(cat);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleEditProductCategory(Arg.Any<IMessageReceiver>(), Arg.Any<EditProductCategoryCmd>());
        }

        [Test]
        public void Dispatch_SendDeleteProductCategoryCommand_HandleDeleteProductCategoryInvoked()
        {
            var cat = Substitute.For<ProductCategory>();
            var cmd = new DeleteProductCategoryCmd(cat);
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleDeleteProductCategory(Arg.Any<IMessageReceiver>(), Arg.Any<DeleteProductCategoryCmd>());
        }

        [Test]
        public void Dispatch_SendRegisterPurchaseCommand_HandleRegisterPurchaseInvoked()
        {
            var cmd = new RegisterPurchaseCmd(new Purchase());
            var msg = new CommandRecievedMsg(1, cmd);

            uut.Dispatch(MainControl.E_COMMAND_RECEIVED, msg);

            handler.Received(1).HandleRegisterPurchase(Arg.Any<IMessageReceiver>(), Arg.Any<RegisterPurchaseCmd>());
        }

        [Test]
        public void Dispatch_SendInvalidEvent_WrittenToLog()
        {
            uut.Dispatch(-1L, null);

            log.Received(1).Write("MainControl", Log.DEBUG, Arg.Any<string>());
        }
    }
}
