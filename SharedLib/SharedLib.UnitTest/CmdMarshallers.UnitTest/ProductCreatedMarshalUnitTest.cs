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
    class ProductCreatedMarshalUnitTest
    {
        Product product;
        ProductCreatedCmd cmd;
        ProductCreatedMarshal pcMarshal;
        string data;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 2,
                ProductNumber = "20"
            };
            cmd = Substitute.For<ProductCreatedCmd>(product);
            pcMarshal = Substitute.For<ProductCreatedMarshal>();
            data = pcMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
            pcMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = pcMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"ProductCreated\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = pcMarshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = pcMarshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectPrice()
        {
            string data = pcMarshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductId()
        {
            string data = pcMarshal.Encode(cmd);

            StringAssert.Contains("ProductId=\"2\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = pcMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectName()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)pcMarshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)pcMarshal.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)pcMarshal.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }

        [Test]
        public void Decode_CorrectProductId()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)pcMarshal.Decode(data);

            Assert.That(decodedCmd.ProductId.Equals(cmd.ProductId));
        }

    }
}
