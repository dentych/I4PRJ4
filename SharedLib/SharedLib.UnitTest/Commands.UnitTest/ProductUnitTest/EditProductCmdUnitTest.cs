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
    class EditProductCmdUnitTest
    {
        Product product;
        EditProductCmd cmd;

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

            cmd = new EditProductCmd("Appelsin", "15", 10, 50, 5);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void EditProductCmd_byAttributesTestName()
        {
            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void EditProductCmd_byAttributesTestProductNumber()
        {
            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void EditProductCmd_byAttributesTestPrice()
        {
            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void EditProductCmd_byAttributesTestProductId()
        {
            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void EditProductCmd_byAttributesTestProductCategoryId()
        {
            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }


        [Test]
        public void EditProductCmd_byProductTestName()
        {
            var cmd = new EditProductCmd(product);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void EditProductCmd_byProductTestProductNumber()
        {
            var cmd = new EditProductCmd(product);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void EditProductCmd_byProductTestPrice()
        {
            var cmd = new EditProductCmd(product);

            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void EditProductCmd_byProductTestProductId()
        {
            var cmd = new EditProductCmd(product);

            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void EditProductCmd_byProductTestProductCategoryId()
        {
            var cmd = new EditProductCmd(product);

            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

    }
}
