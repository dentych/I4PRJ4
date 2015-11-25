﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.CmdMarshallers;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace SharedLib.UnitTest.CmdMarshallers.UnitTest.ProductCategoryUnitTest
{
    [TestFixture]
    class ProductCategoryDeletedMarshalUnitTest
    {
        ProductCategory productCategory;
        ProductCategoryDeletedCmd cmd;
        ProductCategoryDeletedMarshal marshal;
        string data;

        [SetUp]
        public void SetUp()
        {
            productCategory = new ProductCategory() { Name = "Frugt", ProductCategoryId = 5 };

            cmd = Substitute.For<ProductCategoryDeletedCmd>(productCategory);

            marshal = Substitute.For<ProductCategoryDeletedMarshal>();

            data = marshal.Encode(cmd);

        }

        [TearDown]
        public void TearDown()
        {
            productCategory = null;
            cmd = null;
            marshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"ProductCategoryDeleted\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductCategory Name=\"Frugt\"", data);
        }

        [Test]
        public void Encode_ContainsCorrectProductCategoryId()
        {
            var pcdCmd = Substitute.For<ProductCategoryDeletedCmd>(5);

            string data = marshal.Encode(pcdCmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        // ************* DECODE *******************

        [Test]
        public void Decode_CorrectCommandName()
        {
            var decodedCmd = marshal.Decode(data);

            Assert.That(decodedCmd.CmdName.Equals(cmd.CmdName));
        }

        [Test]
        public void Decode_CorrectProductCategoryName()
        {
            ProductCategoryDeletedCmd decodedCmd;
            decodedCmd = (ProductCategoryDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            ProductCategoryDeletedCmd decodedCmd;
            decodedCmd = (ProductCategoryDeletedCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }
    }
}
