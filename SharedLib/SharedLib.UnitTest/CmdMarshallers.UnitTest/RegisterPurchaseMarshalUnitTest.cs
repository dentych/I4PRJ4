using System;
using System.Collections.Generic;
using System.Data;
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
    class RegisterPurchaseMarshalUnitTest
    {

        PurchasedProduct pproduct;
        RegisterPurchaseCmd cmd;
        RegisterPurchaseMarshal rpMarshal;
        private string data;

        [SetUp]
        public void SetUp()
        {
            pproduct = new PurchasedProduct()
            {
                Name = "Banan",
                ProductNumber = "20",
                Quantity = 10,
                UnitPrice = 10,
                PurchasedProductId = 5
            };

            var purchase = new Purchase();
            purchase.PurchasedProducts = new List<PurchasedProduct>();

            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);

            cmd = Substitute.For<RegisterPurchaseCmd>(purchase);

            rpMarshal = Substitute.For<RegisterPurchaseMarshal>();

            data = rpMarshal.Encode(cmd);
        }

        [TearDown]
        public void TearDown()
        {
            pproduct = null;
            cmd = null;
            rpMarshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {

            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"RegisterPurchase\"", data);
        }
        
        [Test]
        public void Encode_ContainsCorrectName()
        {
            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("PurchasedProduct Name=\"Banan\"", data);
        }
        
        [Test]
        public void Encode_ContainsCorrectProductNumber()
        {
            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectQuantity()
        {
            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("Quantity=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectUnitPrice()
        {
            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("UnitPrice=\"10\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectTotalPrice()
        {
            string data = rpMarshal.Encode(cmd);

            StringAssert.Contains("TotalPrice=\"100\"", data);
        }

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = rpMarshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectName()
        {
            RegisterPurchaseCmd decodedCmd;
            decodedCmd = (RegisterPurchaseCmd)rpMarshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).Name.Equals(cmd.Products.ElementAt(1).Name));
        }

        [Test]
        public void Decode_CorrectProductNumber()
        {
            RegisterPurchaseCmd decodedCmd;
            decodedCmd = (RegisterPurchaseCmd)rpMarshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).ProductNumber.Equals(cmd.Products.ElementAt(1).ProductNumber));
        }

        [Test]
        public void Decode_CorrectUnitPrice()
        {
            RegisterPurchaseCmd decodedCmd;
            decodedCmd = (RegisterPurchaseCmd)rpMarshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).UnitPrice.Equals(cmd.Products.ElementAt(1).UnitPrice));
        }

        [Test]
        public void Decode_CorrectQuantity()
        {
            RegisterPurchaseCmd decodedCmd;
            decodedCmd = (RegisterPurchaseCmd)rpMarshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).Quantity.Equals(cmd.Products.ElementAt(1).Quantity));
        }
    }
}
