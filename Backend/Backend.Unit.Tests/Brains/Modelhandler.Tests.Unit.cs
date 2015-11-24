using System.Collections.Generic;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class ModelhandlerUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _ibProtocol = Substitute.For<IBProtocol>();
            _client = Substitute.For<IClient>();
            _uut = new ModelHandler(_ibProtocol, _client);
            _err = Substitute.For<IError>();
        }

        private IBProtocol _ibProtocol;
        private IClient _client;
        private ModelHandler _uut;
        private IError _err;

        [Test]
        public void AddCategory_OwnProduct_ExpectCallTOClient()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _ibProtocol.CategoryXMLParser(tmpCat).Returns("TEST");
            _uut.AddCategory(tmpCat);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void AddCategory_OwnProduct_ExpectCallToProtocolWithProduct()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _uut.AddCategory(tmpCat);
            _ibProtocol.Received(1).CategoryXMLParser(tmpCat);
        }

        [Test]
        public void CatalogueDetails_NoInput_ExpectCallToClient()
        {
            _ibProtocol.GetCatalougXMLParser().Returns("TEST");
            _uut.CatalogueDetails();
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void CatalogueDetails_NoInput_ExpectCallToProtocol()
        {
            _uut.CatalogueDetails();
            _ibProtocol.Received(1).GetCatalougXMLParser();
        }

        [Test]
        public void CreateProduct_NewBadNameProduct_ExpectErrormsg()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            var result = _uut.CreateProduct(tmpproduct);
            _err.Received(1).StdErr("Enter correct product details.");
        }

        [Test]
        public void CreateProduct_NewBadNameProduct_ExpectFail()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            var result = _uut.CreateProduct(tmpproduct);
            Assert.False(result);
        }

        [Test]
        public void CreateProduct_NewBadPriceProduct_ExpectErrormsg()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "1",
                Price = -1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            var result = _uut.CreateProduct(tmpproduct);
            _err.Received(1).StdErr("Enter correct product details.");
        }

        [Test]
        public void CreateProduct_NewBadPriceProduct_ExpectFail()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "1",
                Price = -5,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            var result = _uut.CreateProduct(tmpproduct);
            Assert.False(result);
        }

        [Test]
        public void CreateProduct_NewBadProductnumberProduct_ExpectErrormsg()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "",
                Price = 5,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = ""
            };

            var result = _uut.CreateProduct(tmpproduct);
            _err.Received(1).StdErr("Enter correct product details.");
        }

        [Test]
        public void CreateProduct_NewBadProductNUmberProduct_ExpectFail()
        {
            _uut.Error = _err;
            var tmpproduct = new BackendProduct
            {
                Name = "1",
                Price = 5,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = ""
            };

            var result = _uut.CreateProduct(tmpproduct);
            Assert.False(result);
        }

        [Test]
        public void CreateProduct_NewGoodProduct_ExpectCallToClient()
        {
            var tmpproduct = new BackendProduct
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _ibProtocol.ProductXMLParser(tmpproduct).Returns("TEST");
            _uut.CreateProduct(tmpproduct);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void CreateProduct_NewGoodProduct_ExpectCallToProtocol()
        {
            var tmpproduct = new BackendProduct
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _uut.CreateProduct(tmpproduct);
            _ibProtocol.Received(1).ProductXMLParser(tmpproduct);
        }

        [Test]
        public void DeleteCategory_OwnProduct_ExpectCallTOClient()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _ibProtocol.DeleteCategoryXMLParser(tmpCat).Returns("TEST");
            _uut.DeleteCategory(tmpCat);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void DeleteCategory_OwnProduct_ExpectCallToProtocolWithProduct()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _uut.DeleteCategory(tmpCat);
            _ibProtocol.Received(1).DeleteCategoryXMLParser(tmpCat);
        }

        [Test]
        public void DeleteProduct_OwnProduct_ExpectCallTOClient()
        {
            var tmpproduct = new Product
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _ibProtocol.DeleteProductXMLParser(tmpproduct).Returns("TEST");
            _uut.DeleteProduct(tmpproduct);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void DeleteProduct_OwnProduct_ExpectCallToProtocolWithProduct()
        {
            var tmpproduct = new Product
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _uut.DeleteProduct(tmpproduct);
            _ibProtocol.Received(1).DeleteProductXMLParser(tmpproduct);
        }

        [Test]
        public void EditCategory_OwnProduct_ExpectCallTOClient()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _ibProtocol.EditCategoryXMLParser(tmpCat).Returns("TEST");
            _uut.EditCategory(tmpCat);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void EditCategory_OwnProduct_ExpectCallToProtocolWithProduct()
        {
            var tmpCat = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            _uut.EditCategory(tmpCat);
            _ibProtocol.Received(1).EditCategoryXMLParser(tmpCat);
        }

        [Test]
        public void EditProduct_NewProduct_ExpectCallTOClient()
        {
            var tmpproduct = new BackendProduct
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _ibProtocol.EditProductXMLParser(tmpproduct).Returns("TEST");
            _uut.EditProduct(tmpproduct);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void EditProduct_NewProduct_ExpectCallTOProtocolWithProduct()
        {
            var tmpproduct = new BackendProduct
            {
                Name = "TEST",
                Price = 1,
                ProductCategoryId = 1,
                ProductId = 1,
                ProductNumber = "1"
            };

            _uut.EditProduct(tmpproduct);
            _ibProtocol.Received(1).EditProductXMLParser(tmpproduct);
        }

        [Test]
        public void MoveProductsInCategory_SameId_ExpectFalse()
        {
            var CategoryToEmpty = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 1,
                Products = new List<Product>()
            };

            var CategoryIdToMoveTo = 1;

            Assert.False(_uut.MoveProductsInCategory(CategoryToEmpty, CategoryIdToMoveTo));
        }

        [Test]
        public void MoveProductsInCategory_1ProductInCategory_Expect1CallToProtocol()
        {
            var CategoryToEmpty = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 2,
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        Price = 1,
                        ProductNumber = "123",
                        ProductCategoryId = 2,
                        ProductId = 222
                    }
                }
            };

            var CategoryIdToMoveTo = 1;
            _uut.MoveProductsInCategory(CategoryToEmpty,CategoryIdToMoveTo);
            _ibProtocol.Received(1).EditProductXMLParser(Arg.Any<BackendProduct>());
        }

        [Test]
        public void MoveProductsInCategory_1ProductInCategory_Expect1CallToClient()
        {
            _ibProtocol.EditProductXMLParser(Arg.Any<BackendProduct>()).Returns("TEST");
            var CategoryToEmpty = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 2,
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        Price = 1,
                        ProductNumber = "123",
                        ProductCategoryId = 2,
                        ProductId = 222
                    }
                }
            };

            var CategoryIdToMoveTo = 1;
            _uut.MoveProductsInCategory(CategoryToEmpty, CategoryIdToMoveTo);
            _client.Received(1).Send("TEST");
        }

        [Test]
        public void MoveProductsInCategory_2ProductInCategory_Expect2CallToProtocol()
        {
            var CategoryToEmpty = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 2,
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        Price = 1,
                        ProductNumber = "123",
                        ProductCategoryId = 2,
                        ProductId = 222
                    },
                    new BackendProduct()
                    {
                        Name = "Test2",
                        Price = 1,
                        ProductNumber = "1223",
                        ProductCategoryId = 2,
                        ProductId = 2212
                    }
                }
            };

            var CategoryIdToMoveTo = 1;
            _uut.MoveProductsInCategory(CategoryToEmpty, CategoryIdToMoveTo);
            _ibProtocol.Received(2).EditProductXMLParser(Arg.Any<BackendProduct>());
        }

        [Test]
        public void MoveProductsInCategory_2ProductInCategory_Expect2CallToClient()
        {
            _ibProtocol.EditProductXMLParser(Arg.Any<BackendProduct>()).Returns("TEST");
            var CategoryToEmpty = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 2,
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        Price = 1,
                        ProductNumber = "123",
                        ProductCategoryId = 2,
                        ProductId = 222
                    },
                    new BackendProduct()
                    {
                        Name = "Test2",
                        Price = 1,
                        ProductNumber = "1223",
                        ProductCategoryId = 2,
                        ProductId = 2212
                    }
                }
            };

            var CategoryIdToMoveTo = 1;
            _uut.MoveProductsInCategory(CategoryToEmpty, CategoryIdToMoveTo);
            _client.Received(2).Send("TEST");
        }
    }
}