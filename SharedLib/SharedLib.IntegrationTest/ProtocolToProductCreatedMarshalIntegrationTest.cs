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

namespace SharedLib.IntegrationTest
{
    [TestFixture]
    class ProtocolToProductCreatedMarshalIntegrationTest
    {
        Product product;
        private Protocol.Protocol protocol;
        ProductCreatedCmd cmd;
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
            protocol = new Protocol.Protocol();
            cmd = new ProductCreatedCmd(product);
            data = protocol.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            protocol = null;
            cmd = null;
            data = null;
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectCommandName()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("Command Name=\"ProductCreated\"", data);
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectName()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectProductNumber()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectPrice()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectProductId()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("ProductId=\"2\"", data);
        }

        [Test]
        public void Protocol_Encode_ContainsCorrectProductCategoryId()
        {
            string data = protocol.Encode(cmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        [Test]
        public void Protocol_Decode_CorrectCommandName()
        {
            var decodedCmd = protocol.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Protocol_Decode_CorrectName()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)protocol.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Protocol_Decode_CorrectProductNumber()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)protocol.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Protocol_Decode_CorrectPrice()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)protocol.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }

        [Test]
        public void Protocol_Decode_CorrectProductId()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)protocol.Decode(data);

            Assert.That(decodedCmd.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void Protocol_Decode_CorrectProductCategoryId()
        {
            ProductCreatedCmd decodedCmd;
            decodedCmd = (ProductCreatedCmd)protocol.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }
    }
}
