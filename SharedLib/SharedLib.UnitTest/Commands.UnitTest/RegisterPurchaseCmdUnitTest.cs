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
    class RegisterPurchaseCmdUnitTest
    {
        PurchasedProduct pproduct;
        RegisterPurchaseCmd cmd;

        [SetUp]
        public void SetUp()
        {
            pproduct = new PurchasedProduct()
            {
                Name = "Banan",
                UnitPrice = 10,
                PurchasedProductId = 1,
                ProductNumber = "20",
                Quantity = 2
            };

            var purchase = new Purchase();
            purchase.PurchasedProducts = new List<PurchasedProduct>();

            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);

            cmd = Substitute.For<RegisterPurchaseCmd>(purchase);
        }

        [TearDown]
        public void TearDown()
        {
            pproduct = null;
            cmd = null;
        }

        [Test]
        public void RegisterPurchaseCmd_CorrectName()
        {
            Assert.That(cmd.Products.ElementAt(1).Name.Equals(pproduct.Name));
        }

        [Test]
        public void RegisterPurchaseCmd_CorrectUnitPrice()
        {
            Assert.That(cmd.Products.ElementAt(1).UnitPrice.Equals(pproduct.UnitPrice));
        }

        [Test]
        public void RegisterPurchaseCmd_CorrectPurchasedProductId()
        {
            Assert.That(cmd.Products.ElementAt(1).PurchasedProductId.Equals(pproduct.PurchasedProductId));
        }

        [Test]
        public void RegisterPurchaseCmd_CorrectProductNumber()
        {
            Assert.That(cmd.Products.ElementAt(1).ProductNumber.Equals(pproduct.ProductNumber));
        }

        [Test]
        public void RegisterPurchaseCmd_CorrectQuantity()
        {
            Assert.That(cmd.Products.ElementAt(1).Quantity.Equals(pproduct.Quantity));
        }
    }
}
