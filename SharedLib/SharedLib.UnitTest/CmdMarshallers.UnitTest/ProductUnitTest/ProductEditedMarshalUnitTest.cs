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
    class ProductEditedMarshalUnitTest
    {
        Product product;
        ProductEditedCmd cmd;
        ProductEditedMarshal marshal;
        string data;
        int OldCategoryId;

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
            OldCategoryId = 8;
            cmd = Substitute.For<ProductEditedCmd>(product, OldCategoryId);
            marshal = Substitute.For<ProductEditedMarshal>();
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

            StringAssert.Contains("Command Name=\"ProductEdited\"", data);
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
        public void Encode_ContainsCorrectOldProductCategoryId()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("OldProductCategoryId=\"8\"", data);
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
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }

        [Test]
        public void Decode_CorrectProductId()
        {
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

        [Test]
        public void Decode_CorrectOldProductCategoryId()
        {
            ProductEditedCmd decodedCmd;
            decodedCmd = (ProductEditedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.OldProductCategoryId.Equals(cmd.OldProductCategoryId));
        }
    }
}
