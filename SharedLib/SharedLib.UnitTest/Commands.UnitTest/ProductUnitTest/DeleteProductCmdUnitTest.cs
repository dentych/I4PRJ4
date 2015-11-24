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
    class DeleteProductCmdUnitTest
    {
        Product product;
        DeleteProductCmd cmd;

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

            cmd = new DeleteProductCmd(50);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void DeleteProductCmd_byAttributesTestProductId()
        {
            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }


        [Test]
        public void DeleteProductCmd_byProductTestName()
        {
            var cmd = new DeleteProductCmd(product);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void DeleteProductCmd_byProductTestProductNumber()
        {
            var cmd = new DeleteProductCmd(product);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void DeleteProductCmd_byProductTestPrice()
        {
            var cmd = new DeleteProductCmd(product);

            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void DeleteProductCmd_byProductTestProductId()
        {
            var cmd = new DeleteProductCmd(product);

            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void DeleteProductCmd_byProductTestProductCategoryId()
        {
            var cmd = new DeleteProductCmd(product);

            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }
    }
}
