using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class ProductCategoryListTest
    {
        ProductCategoryList uut;

        [SetUp]
        public void SetUp()
        {
            uut = new ProductCategoryList();
            uut._db = Substitute.For<IDBcontrol>();
            uut._db.GetProducts().Returns(new List<ProductCategory>());
        }

        [Test]
        public void Construct_1_Expect1call()
        {
            uut.Update();
            uut._db.Received().GetProducts();
        }
    }
}
