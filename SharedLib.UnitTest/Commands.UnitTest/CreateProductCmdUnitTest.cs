using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest.Commands.UnitTest
{
    [TestFixture]
    class CreateProductCmdUnitTest
    {
        Product product;
        CreateProductCmd cmd;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Appelsin",
                Price = 10,
                ProductId = 50,
                ProductNumber = "15"
            };

            cmd = new CreateProductCmd("Appelsin","15",10);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void CreateProductCmd_byAttributesTestName()
        {
            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void CreateProductCmd_byAttributesTestProductNumber()
        {
            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void CreateProductCmd_byAttributesTestPrice()
        {
            Assert.That(product.Price.Equals(cmd.Price));
        }


        [Test]
        public void CreateProductCmd_byProductTestName()
        {
            var cmd = new CreateProductCmd(product);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void CreateProductCmd_byProductTestProductNumber()
        {
            var cmd = new CreateProductCmd(product);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void CreateProductCmd_byProductTestPrice()
        {
            var cmd = new CreateProductCmd(product);

            Assert.That(product.Price.Equals(cmd.Price));
        }

    }
}
