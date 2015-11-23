using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Datamodels;
using Backend.Models.SocketEvents;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol.Commands.ProductCategoryCommands;

namespace Backend.Unit.Tests.SocketEvents
{
    [TestFixture]
    public class SocketEventsUnitTests
    {
        private BackendProductCategoryList _categories;
        private BackendProductCategory _categoryWithNoProducts;
        private BackendProductCategory _categoryWithProducts;
        private SocketEventHandlers _uut;
        private Product _product1;
        private Product _product2;

        [SetUp]
        public void SetUp()
        {
            _categories = new BackendProductCategoryList();

            //Category with two products
            _categoryWithProducts = new BackendProductCategory
            {
                Name = "CategoryWithProducts",
                ProductCategoryId = 0
            };

            //Category with no products
            _categoryWithNoProducts = new BackendProductCategory()
            {
                Name = "CategoryWithNoProducts",
                ProductCategoryId = 1
            };

            // Product 1
            _product1 = new Product()
            {
                Name = "Product1",
                Price = 1,
                ProductCategoryId = 0,
                ProductId = 0,
                ProductNumber = "P1"
            };

            // Product 2
            _product2 = new Product()
            {
                Name = "Product2",
                Price = 2,
                ProductCategoryId = 0,
                ProductId = 1,
                ProductNumber = "P2"
            };
            // Add products
            _categoryWithProducts.AddProduct(_product1);
            _categoryWithProducts.AddProduct(_product2);

            //Add Categories
            _categories.Add(_categoryWithNoProducts);
            _categories.Add(_categoryWithProducts);

            // Setting the uut to use _categories, which we should now hold a reference to.
            _uut = new SocketEventHandlers(_categories); 

        }

        [Test]
        public void ProductCreatedHandler_OneProductToListWithNoProducts_AssertProductAdded()
        {
            //Product to add

            var testcommand = new ProductCreatedCmd("TEST", "TEST1234", 1234, 3, _categoryWithNoProducts.ProductCategoryId);

            _uut.ProductCreatedHandler(testcommand);

            Assert.That(_categoryWithNoProducts.Products.Count,Is.EqualTo(1));
        }

        [Test]
        public void ProductCreatedHandler_OneProductToListWithNoProducts_AssertCorrectProductAdded()
        {
            //Product to add

            var testproduct = new Product()
            {
                Name = "TEST",
                Price = 1234,
                ProductCategoryId = _categoryWithNoProducts.ProductCategoryId,
                ProductId = 3,
                ProductNumber = "TEST1234"
            };
            var testcommand = new ProductCreatedCmd(testproduct.Name, testproduct.ProductNumber, testproduct.Price,
                testproduct.ProductId, testproduct.ProductCategoryId);

            //todo Der må fandme være noget smartere. 
            _uut.ProductCreatedHandler(testcommand);
            Assert.That(_categoryWithNoProducts.Products[0].Name, Is.EqualTo(testproduct.Name));
            Assert.That(_categoryWithNoProducts.Products[0].ProductNumber, Is.EqualTo(testproduct.ProductNumber));
            Assert.That(_categoryWithNoProducts.Products[0].Price, Is.EqualTo(testproduct.Price));
            Assert.That(_categoryWithNoProducts.Products[0].ProductId, Is.EqualTo(testproduct.ProductId));
            Assert.That(_categoryWithNoProducts.Products[0].ProductCategoryId, Is.EqualTo(testproduct.ProductCategoryId));
        }

        [Test]
        public void ProductDeletedHandler_DeleteFromListWithProducts_AssertCountIs1()
        {
            var testcommand = new ProductDeletedCmd(_product2.ProductId);
            _uut.ProductDeletedHandler(testcommand);

            Assert.That(_categoryWithProducts.Products.Count,Is.EqualTo(1));
        }

        [Test]
        public void ProductDeletedHandler_DeleteFromListWithProducts_AssertCorrectProductIsDeleted()
        {
            var testcommand = new ProductDeletedCmd(_product2.ProductId);
            _uut.ProductDeletedHandler(testcommand);

            Assert.That(_categoryWithProducts.Products[0].Name, Is.EqualTo(_product1.Name));
        }

        [Test]
        public void ProductEditedHandler_EditProduct1_AssertProductEdited()
        {
            var productedited = _product1;
            _product1.Name = "Edited";

            var testCommand = new ProductEditedCmd(productedited,_product1.ProductCategoryId);
            _uut.ProductEditedHandler(testCommand);

            Assert.That(_categoryWithProducts.Products[0].Name, Is.EqualTo(productedited.Name));
        }

        [Test]
        public void ProductEditedHandler_EditProduct1_AssertNoProductsAdded()
        {
            var productedited = _product1;
            _product1.Name = "Edited";

            var testCommand = new ProductEditedCmd(productedited, _product1.ProductCategoryId);
            _uut.ProductEditedHandler(testCommand);

            Assert.That(_categoryWithProducts.Products.Count,Is.EqualTo(2));
        }

        [Test]
        public void CatalogueDetailsHandler_CreatingNewListClearingOld_AssertCategoryCountIs1()
        {
            _categories.Clear();
            var cats = new List<ProductCategory>();
            var newcat = new ProductCategory(new List<Product>())
            {
                Name = "TESTCAT",
                ProductCategoryId = 8,
                Products = new List<Product>()
            };
            cats.Add(newcat);
            
            var testcommand = new CatalogueDetailsCmd(cats);
            _uut.CatalogueDetailsHandler(testcommand);

            Assert.That(_categories.Count,Is.EqualTo(1));
        }

        [Test]
        public void CatalogueDetailsHandler_CreatingNewListClearingOld_AssertCategoryCorrectAdded()
        {
            _categories.Clear();
            var cats = new List<ProductCategory>();
            var newcat = new ProductCategory(new List<Product>())
            {
                Name = "TESTCAT",
                ProductCategoryId = 8,
                Products = new List<Product>()
            };
            cats.Add(newcat);

            var testcommand = new CatalogueDetailsCmd(cats);
            _uut.CatalogueDetailsHandler(testcommand);

            Assert.That(_categories[0].Name, Is.EqualTo(newcat.Name));
            Assert.That(_categories[0].ProductCategoryId, Is.EqualTo(newcat.ProductCategoryId));
            Assert.That(_categories[0].Products, Is.EqualTo(newcat.Products));

        }

        [Test]
        public void ProductCategoryCreatedHandler_CreateCategory_AssertCountIsThree()
        {
            var newcat = new ProductCategory(new List<Product>())
            {
                Name = "TESTCAT",
                ProductCategoryId = 8,
                Products = new List<Product>()
            };
            var testcommand = new ProductCategoryCreatedCmd(newcat);

            _uut.ProductCategoryCreatedHandler(testcommand);
            Assert.That(_categories.Count, Is.EqualTo(3));
        }

        [Test]
        public void ProductCategoryCreatedHandler_CreateCategory_AssertCategoryCorrectAdded()
        {
            var newcat = new ProductCategory(new List<Product>())
            {
                Name = "TESTCAT",
                ProductCategoryId = 8,
                Products = new List<Product>()
            };
            var testcommand = new ProductCategoryCreatedCmd(newcat);

            _uut.ProductCategoryCreatedHandler(testcommand);
            Assert.That(_categories[2].Name, Is.EqualTo(newcat.Name));
            Assert.That(_categories[2].ProductCategoryId, Is.EqualTo(newcat.ProductCategoryId));
            Assert.That(_categories[2].Products, Is.EqualTo(newcat.Products));

        }

        [Test]
        public void ProductCategoryDeletedHandler_DeleteCategoryWithProducts_ExpectCategoryToBeDeleted()
        {
            var testcommand = new ProductCategoryDeletedCmd(_categoryWithProducts);
            _uut.ProductCategoryDeletedHandler(testcommand);

            Assert.That(_categories.Count,Is.EqualTo(1));
        }

        [Test]
        public void ProductCategoryDeletedHandler_DeleteCategoryWithProducts_ExpectCorrectCategoryToBeDeleted()
        {
            var testcommand = new ProductCategoryDeletedCmd(_categoryWithProducts);
            _uut.ProductCategoryDeletedHandler(testcommand);

            Assert.That(_categories[0], Is.EqualTo(_categoryWithNoProducts));
        }

        [Test]
        public void ProductCategoryEditedHandler_EditcategoryWithProducts_ExpectEditToHappen()
        {
            var edited = new ProductCategory()
            {
                Name = "Edited",
                ProductCategoryId = _categoryWithProducts.ProductCategoryId,
                Products = _categoryWithProducts.Products
            };

            var testcommand = new ProductCategoryEditedCmd(edited);
            _uut.ProductCategoryEditedHandler(testcommand);

            Assert.That(_categories[1].Name, Is.EqualTo(edited.Name));
        }
    }
}
