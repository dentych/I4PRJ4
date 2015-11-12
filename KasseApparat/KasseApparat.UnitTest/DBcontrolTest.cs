using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class DBcontrolTest
    {
        DBcontrol uut;

        [SetUp]
        public void SetUp()
        {
            uut = new DBcontrol(null);
            uut.Connection = Substitute.For<IConnection>();
            uut.protocol = Substitute.For<IProtocol>();
        }

        [Test]
        public void GetProducts_ConnectionSend_Expect1Call()
        {
            uut.protocol.Decode(Arg.Any<string>()).Returns(new CatalogueDetailsCmd());

            uut.GetProducts();

            uut.Connection.Received(1).Send(Arg.Any<string>());
        }

        [Test]
        public void GetProducts_ConnectionReceive_Expect1Call()
        {
            uut.protocol.Decode(Arg.Any<string>()).Returns(new CatalogueDetailsCmd());

            uut.GetProducts();

            uut.Connection.Received(1).Receive();
        }

        [Test]
        public void PurchaseDone_ConnectionSend_Expect1Call()
        {
            uut.PurchaseDone(new List<PurchasedProduct>());

            uut.Connection.Received(1).Send(Arg.Any<string>());
        }
    }
}
