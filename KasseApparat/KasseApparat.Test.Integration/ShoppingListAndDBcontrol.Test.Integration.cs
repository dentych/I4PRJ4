using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace KasseApparat.Test.Integration
{
    [TestFixture]
    class ShoppingListAndDBcontrol
    {
        ShoppingList SLut;
        DBcontrol DBCut;
        IConnection FakeConnection;

        [SetUp]
        public void SetUp()
        {
            FakeConnection = Substitute.For<IConnection>();

            SLut = new ShoppingList();
            DBCut = new DBcontrol(FakeConnection);

            DBCut.protocol = Substitute.For<IProtocol>();
            SLut._db = DBCut;
            SLut.print = Substitute.For<IPrinter>();

            IEnumerable<Command> IE = new Command[] { new CatalogueDetailsCmd() };
            DBCut.protocol.GetCommands().Returns(IE);

            SLut.AddItem(new PurchasedProduct());
        }

        [Test]
        public void ConnectionConnect_1Call_Expect1()
        {
            SLut.EndPurchase();

            FakeConnection.Received(1).Connect();
        }

        [Test]
        public void ConnectionDisconnect_1Call_Expect1()
        {
            SLut.EndPurchase();

            FakeConnection.Received(1).Disconnect();
        }

        [Test]
        public void ConnectionSend_1Call_Expect1()
        {
            SLut.EndPurchase();

            FakeConnection.Received(1).Send(Arg.Any<string>());
        }
    }
}
