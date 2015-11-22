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

namespace SharedLib.UnitTest.CmdMarshallers.UnitTest
{
    [TestFixture]
    class CatalogueDetailsMarshalUnitTest
    {
        ProductCategory productCategory;
        Product product;
        CatalogueDetailsCmd cmd;
        CatalogueDetailsMarshal cdMarshal;
        string data;

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

            productCategory = new ProductCategory() { Name = "Frugt", ProductCategoryId = 5 };

            productCategory.Products.Add(product);
            productCategory.Products.Add(product);
            productCategory.Products.Add(product);

            cmd = Substitute.For<CatalogueDetailsCmd>();
            cmd.ProductCategories.Add(productCategory);
            cmd.ProductCategories.Add(productCategory);
            cmd.ProductCategories.Add(productCategory);
            cdMarshal = Substitute.For<CatalogueDetailsMarshal>();
            data = cdMarshal.Encode(cmd);

        }

        [TearDown]
        public void TearDown()
        {
            productCategory = null;
            product = null;
            cmd = null;
            cdMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"CatalogueDetails\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryName()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("ProductCategory Name=\"Frugt\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryId()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectPrice()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductId()
        {
            string data = cdMarshal.Encode(cmd);

            StringAssert.Contains("ProductId=\"1\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = cdMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectProductCategoryName()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategories.ElementAt(1).Name.Equals(cmd.ProductCategories.ElementAt(1).Name));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);
            
            Assert.That(decodedCmd.ProductCategories.ElementAt(1).ProductCategoryId.Equals(cmd.ProductCategories.ElementAt(1).ProductCategoryId));
        }

        [Test]
        public void Decode_CorrectName()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Name.Equals(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductNumber.Equals(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Price.Equals(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Price));
        }

        [Test]
        public void Decode_CorrectProductId()
        {
            CatalogueDetailsCmd decodedCmd;
            decodedCmd = (CatalogueDetailsCmd)cdMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductId.Equals(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductId));
        }

    }
}
