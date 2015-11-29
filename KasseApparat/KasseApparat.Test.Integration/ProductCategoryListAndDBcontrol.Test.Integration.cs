using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace KasseApparat.Test.Integration
{
    class ProductCategoryListAndDBcontrol
    {
        ProductCategoryList PCLut;
        DBcontrol DBCut;
        IConnection FakeConnection ;

        [SetUp]
        public void SetUp()
        {
            FakeConnection = Substitute.For<IConnection>();

            PCLut = new ProductCategoryList();
            DBCut = new DBcontrol(FakeConnection);

            DBCut.protocol = Substitute.For<IProtocol>();
            PCLut._db = DBCut;

            IEnumerable<Command> IE = new Command[] { new CatalogueDetailsCmd() };
            DBCut.protocol.GetCommands().Returns(IE);
        }

        [Test]
        public void ConnectionConnect_1Call_Expect1()
        {
            PCLut.Update();

            FakeConnection.Received(1).Connect();
        }

        [Test]
        public void ConnectionDisconnect_1Call_Expect1()
        {
            PCLut.Update();

            FakeConnection.Received(1).Disconnect();
        }

        [Test]
        public void ConnectionSend_1Call_Expect1()
        {
            PCLut.Update();

            FakeConnection.Received(1).Send(Arg.Any<string>());
        }

        [Test]
        public void ConnectionReceive_1Call_Expect1()
        {
            PCLut.Update();

            FakeConnection.Received(1).Receive();
        }
    }
}
