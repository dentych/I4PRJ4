using System;
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
    class EditProductCategoryMarshalUnitTest
    {
        ProductCategory productCategory;
        Product product;
        EditProductCategoryCmd cmd;
        EditProductCategoryMarshal marshal;
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

            productCategory = new ProductCategory() { Name = "Frugt", ProductCategoryId = 5};

            productCategory.Products.Add(product);
            productCategory.Products.Add(product);
            productCategory.Products.Add(product);

            cmd = Substitute.For<EditProductCategoryCmd>(productCategory);

            marshal = Substitute.For<EditProductCategoryMarshal>();
            data = marshal.Encode(cmd);

        }

        [TearDown]
        public void TearDown()
        {
            productCategory = null;
            product = null;
            cmd = null;
            marshal = null;
            data = null;
        }

        [Test]
        public void Encode_ContainsCorrectCommandName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Command Name=\"EditProductCategory\"", data);
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
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductCategoryId=\"5\"", data);
        }

        // ************* Encode Products ***************

        [Test]
        public void Encode_ProductContainsCorrectName()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Product Name=\"Banan\"", data);
        }

        [Test]
        public void Encode_ProductContainsCorrectProductNumber()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductNumber=\"20\"", data);
        }

        [Test]
        public void Encode_ProductContainsCorrectPrice()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("Price=\"10\"", data);
        }

        [Test]
        public void Encode_ProductContainsCorrectProductId()
        {
            string data = marshal.Encode(cmd);

            StringAssert.Contains("ProductId=\"1\"", data);
        }

        [Test]
        public void Encode_ProductContainsCorrectProductCategoryId()
        {
            string data = marshal.Encode(cmd);

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
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Name.Equals(cmd.Name));
        }

        [Test]
        public void Decode_CorrectProductCategoryId()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.ProductCategoryId.Equals(cmd.ProductCategoryId));
        }

        // *********** Decode Products ****************

        [Test]
        public void Decode_ProductCorrectName()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).Name.Equals(cmd.Products.ElementAt(1).Name));
        }

        [Test]
        public void Decode_ProductCorrectProductNumber()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).ProductNumber.Equals(cmd.Products.ElementAt(1).ProductNumber));
        }

        [Test]
        public void Decode_ProductCorrectPrice()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).Price.Equals(cmd.Products.ElementAt(1).Price));
        }

        [Test]
        public void Decode_ProductCorrectProductId()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).ProductId.Equals(cmd.Products.ElementAt(1).ProductId));
        }

        [Test]
        public void Decode_ProductCorrectProductCategoryId()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(decodedCmd.Products.ElementAt(1).ProductCategoryId.Equals(cmd.Products.ElementAt(1).ProductCategoryId));
        }

        [Test]
        public void Decode_MultipleCorrectProducts()
        {
            EditProductCategoryCmd decodedCmd;
            decodedCmd = (EditProductCategoryCmd)marshal.Decode(data);

            Assert.That(cmd.Products.ElementAt(0).Name.Equals(decodedCmd.Products.ElementAt(0).Name));
            Assert.That(decodedCmd.Products.ElementAt(0).Name.Equals(decodedCmd.Products.ElementAt(1).Name));
            Assert.That(decodedCmd.Products.ElementAt(1).Name.Equals(decodedCmd.Products.ElementAt(2).Name));
        }
    }
}
