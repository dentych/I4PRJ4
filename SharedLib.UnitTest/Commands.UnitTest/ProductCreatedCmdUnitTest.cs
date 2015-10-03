﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.Commands;

namespace SharedLib.UnitTest.Commands.UnitTest
{
    [TestFixture]
    class ProductCreatedCmdUnitTest
    {
        Product product;
        ProductCreatedCmd cmd;

        [SetUp]
        public void SetUp()
        {
            product = new Product()
            {
                Name = "Appelsin",
                Price = 10,
                ProductId = 50,
                ProductNumber = "15"
            };

            cmd = new ProductCreatedCmd("Appelsin", "15", 10,50);
        }

        [TearDown]
        public void TearDown()
        {
            product = null;
            cmd = null;
        }

        [Test]
        public void ProductCreatedCmd_byAttributesTestName()
        {
            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void ProductCreatedCmd_byAttributesTestProductNumber()
        {
            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void ProductCreatedCmd_byAttributesTestPrice()
        {
            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void ProductCreatedCmd_byAttributesTestProductId()
        {
            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }

        [Test]
        public void ProductCreatedCmd_byProductTestName()
        {
            var cmd = new ProductCreatedCmd(product);

            Assert.That(product.Name.Equals(cmd.Name));
        }

        [Test]
        public void ProductCreatedCmd_byProductTestProductNumber()
        {
            var cmd = new ProductCreatedCmd(product);

            Assert.That(product.ProductNumber.Equals(cmd.ProductNumber));
        }

        [Test]
        public void ProductCreatedCmd_byProductTestPrice()
        {
            var cmd = new ProductCreatedCmd(product);

            Assert.That(product.Price.Equals(cmd.Price));
        }

        [Test]
        public void ProductCreatedCmd_byProductTestProductId()
        {
            var cmd = new ProductCreatedCmd(product);

            Assert.That(product.ProductId.Equals(cmd.ProductId));
        }
    }
}
