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
    class DeleteProductMarshalUnitTest
    {
        Product product;
        DeleteProductCmd cmd;
        DeleteProductMarshal dpMarshal;
        string data;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 1,
                ProductNumber = "20",
                ProductCategoryId = 5

            };
            cmd = Substitute.For<DeleteProductCmd>(product);
            dpMarshal = Substitute.For<DeleteProductMarshal>();
            data = dpMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
            dpMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = dpMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"DeleteProduct\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = dpMarshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = dpMarshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectPrice()
        {
            string data =dpMarshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductId()
        {
            string data = dpMarshal.Encode(cmd);

            StringAssert.Contains("ProductId=\"1\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryId()
        {
            string data = dpMarshal.Encode(cmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = dpMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectName()
        {
            DeleteProductCmd decodedCmd;
            decodedCmd = (DeleteProductCmd)dpMarshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            DeleteProductCmd decodedCmd;
            decodedCmd = (DeleteProductCmd)dpMarshal.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            DeleteProductCmd decodedCmd;
            decodedCmd = (DeleteProductCmd)dpMarshal.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }

        [Test]
        public void Decode_CorrectProductId()
        {
            DeleteProductCmd decodedCmd;
            decodedCmd = (DeleteProductCmd)dpMarshal.Decode(data);

            Assert.That(decodedCmd.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            DeleteProductCmd decodedCmd;
            decodedCmd = (DeleteProductCmd)dpMarshal.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }
    }
}
