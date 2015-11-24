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
    class ProductEditedCmdUnitTest
    {
        Product product;
        ProductEditedCmd cmd;
        int OldProductCategoryId;

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

            OldProductCategoryId = 8;

            cmd = new ProductEditedCmd("Appelsin", "15", 10, 50, 5, OldProductCategoryId);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestName()
        {
            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestProductNumber()
        {
            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestPrice()
        {
            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestProductId()
        {
            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestProductCategoryId()
        {
            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

        [Test]
        public void ProductEditedCmd_byAttributesTestOldProductCategoryId()
        {
            Assert.That(cmd.OldProductCategoryId.Equals(OldProductCategoryId));
        }


        [Test]
        public void ProductEditedCmd_byProductTestName()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void ProductEditedCmd_byProductTestProductNumber()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void ProductEditedCmd_byProductTestPrice()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void ProductEditedCmd_byProductTestProductId()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void ProductEditedCmd_byProductTestProductCategoryId()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(product.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

        [Test]
        public void ProductEditedCmd_byProductTestOldProductCategoryId()
        {
            var cmd = new ProductEditedCmd(product, OldProductCategoryId);

            Assert.That(cmd.OldProductCategoryId.Equals(OldProductCategoryId));
        }
    }
}
