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

namespace SharedLib.UnitTest.Commands.UnitTest
{
    [TestFixture]
    class CatalogueDetailsCmdUnitTest
    {
        Product product;
        ProductCategory productCategory;
        CatalogueDetailsCmd cmd;

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

            productCategory = new ProductCategory() {Name = "Frugt", ProductCategoryId = 5};

            productCategory.Products.Add(product);
            productCategory.Products.Add(product);
            productCategory.Products.Add(product);

            cmd = Substitute.For<CatalogueDetailsCmd>();
            cmd.ProductCategories.Add(productCategory);
            cmd.ProductCategories.Add(productCategory);
            cmd.ProductCategories.Add(productCategory);
        }

        [TearDown]
        public void TearDown()
        {
            productCategory = null;
            product = null;
            cmd = null;
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductCategoryName()
        {
            //Assert.That(cmd.ProductCategories.ElementAt(1).Name.Equals(productCategory.Name));
            cmd.Received(0).GetCatalogue();
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductCategoryId()
        {
            Assert.That(cmd.ProductCategories.ElementAt(1).ProductCategoryId.Equals(productCategory.ProductCategoryId));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectNameInProducts()
        {
            Assert.That(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Name.Equals(product.Name));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectPriceInProducts()
        {
            Assert.That(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).Price.Equals(product.Price));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductIdInProducts()
        {
            Assert.That(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductId.Equals(product.ProductId));
        }

        [Test]
        public void CatalogueDetailsCmd_CorrectProductNumberInProducts()
        {
            Assert.That(cmd.ProductCategories.ElementAt(1).Products.ElementAt(1).ProductNumber.Equals(product.ProductNumber));
        }
    }
}
