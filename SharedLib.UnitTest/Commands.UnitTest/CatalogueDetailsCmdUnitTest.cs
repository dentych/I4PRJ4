using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest.Commands.UnitTest
{
    [TestFixture]
    class CatalogueDetailsCmdUnitTest
    {
        Product product;
        CatalogueDetailsCmd cmd;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 1,
                ProductNumber = "20"
            };
            cmd = Substitute.For<CatalogueDetailsCmd>();
            cmd.Products.Add(product);
            cmd.Products.Add(product);
            cmd.Products.Add(product);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectName()
        {
            Assert.That(cmd.Products.ElementAt(1).Name.Equals(product.Name));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectPrice()
        {
            Assert.That(cmd.Products.ElementAt(1).Price.Equals(product.Price));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductId()
        {
            Assert.That(cmd.Products.ElementAt(1).ProductId.Equals(product.ProductId));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductNumber()
        {
            Assert.That(cmd.Products.ElementAt(1).ProductNumber.Equals(product.ProductNumber));
        }
    }
}
