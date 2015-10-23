using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    public class ProductListTest
    {
        [Test]
        public void Update_getList_ExpectedBeer()
        {
            var uut = new ProductList();
            uut.Db = new FakeDBcontrol();

            uut.Update();

            Assert.That(uut[0].Name, Is.EqualTo("Beer"));
        }

        [Test]
        public void Update_getList_ExpectedMorganOgCola()
        {
            var uut = new ProductList();
            uut.Db = new FakeDBcontrol();

            uut.Update();

            Assert.That(uut[4].Name, Is.EqualTo("Morgan og Cola"));
        }
    }
}
