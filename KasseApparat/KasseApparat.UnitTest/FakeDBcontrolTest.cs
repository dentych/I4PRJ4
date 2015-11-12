using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharedLib.Models;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class FakeDBcontrolTest
    {
        IDBcontrol uut;

        [SetUp]
        public void SetUp()
        {
            uut = new FakeDBcontrol();
        }

        [Test]
        public void GetProducts_1Call_Expect()
        {
            uut.PurchaseDone(new ShoppingList());
            List<ProductCategory> l = uut.GetProducts();
            Assert.That(l.Count, Is.EqualTo(2));
        }
    }
}
