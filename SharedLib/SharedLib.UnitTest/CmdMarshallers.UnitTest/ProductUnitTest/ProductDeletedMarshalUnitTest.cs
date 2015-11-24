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

namespace SharedLib.UnitTest.CmdMarshallers.UnitTest.ProductUnitTest
{
    [TestFixture]
    class ProductDeletedMarshalUnitTest
    {
        Product product;
        ProductDeletedCmd cmd;
        ProductDeletedMarshal marshal;
        string data;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 2,
                ProductNumber = "20",
                ProductCategoryId = 5
            };
            cmd = Substitute.For<ProductDeletedCmd>(product);
            marshal = Substitute.For<ProductDeletedMarshal>();
            data = marshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
            marshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"ProductDeleted\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectPrice()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductId()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductId=\"2\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryId()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = marshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectName()
        {
            ProductDeletedCmd decodedCmd;
            decodedCmd = (ProductDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            ProductDeletedCmd decodedCmd;
            decodedCmd = (ProductDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            ProductDeletedCmd decodedCmd;
            decodedCmd = (ProductDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }

        [Test]
        public void Decode_CorrectProductId()
        {
            ProductDeletedCmd decodedCmd;
            decodedCmd = (ProductDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            ProductDeletedCmd decodedCmd;
            decodedCmd = (ProductDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

    }
}
