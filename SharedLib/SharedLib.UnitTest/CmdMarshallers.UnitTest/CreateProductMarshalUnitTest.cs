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
    class CreateProductMarshalUnitTest
    {
        Product product;
        CreateProductCmd cmd;
        CreateProductMarshal cpMarshal;
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
            cmd = Substitute.For<CreateProductCmd>(product);
            cpMarshal = Substitute.For<CreateProductMarshal>();
            data = cpMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
            cpMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = cpMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"CreateProduct\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = cpMarshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"",data);
        }

        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = cpMarshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectPrice()
        {
            string data = cpMarshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = cpMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectName()
        {
            CreateProductCmd decodedCmd;
            decodedCmd = (CreateProductCmd)cpMarshal.Decode(data);
            
            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            CreateProductCmd decodedCmd;
            decodedCmd = (CreateProductCmd)cpMarshal.Decode(data);

            Assert.That(decodedCmd.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void Decode_CorrectPrice()
        {
            CreateProductCmd decodedCmd;
            decodedCmd = (CreateProductCmd)cpMarshal.Decode(data);

            Assert.That(decodedCmd.Price.Equals(cmd.Price));
        }
    }
}
