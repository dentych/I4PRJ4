using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest.Commands.UnitTest.ProductUnitTest
{
    [TestFixture]
    class ProductDeletedCmdUnitTest
    {
        Product product;
        ProductDeletedCmd cmd;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Appelsin",
                Price = 10,
                ProductId = 50,
                ProductNumber = "15",
                ProductCategoryId = 5
            };

            cmd = new ProductDeletedCmd(50);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void ProductDeletedCmd_byAttributesTestProductId()
        {
            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }


        [Test]
        public void ProductDeletedCmd_byProductTestName()
        {
            var cmd = new ProductDeletedCmd(product);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void ProductDeletedCmd_byProductTestProductNumber()
        {
            var cmd = new ProductDeletedCmd(product);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void ProductDeletedCmd_byProductTestPrice()
        {
            var cmd = new ProductDeletedCmd(product);

            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void ProductDeletedCmd_byProductTestProductId()
        {
            var cmd = new ProductDeletedCmd(product);

            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void ProductDeletedCmd_byProductTestProductCategoryId()
        {
            var cmd = new ProductDeletedCmd(product);

            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }
    }
}
