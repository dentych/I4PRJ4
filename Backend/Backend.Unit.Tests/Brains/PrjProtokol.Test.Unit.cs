using Backend.Models.Brains;
using Backend.Models.Datamodels;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class PrjProtocolUnitTests
    {
        [SetUp]
        public void SetUp()
        {
            _uut = new PrjProtokol();
            _protocol = Substitute.For<IProtocol>();
            _uut.LocalProtocol = _protocol;
            _testProduct = new Product();
            _btestProduct = new BackendProduct();
            _testCategory = new BackendProductCategory();
            _protocol.Encode(Arg.Any<Command>()).Returns("TEST");
        }

        private PrjProtokol _uut;
        private IProtocol _protocol;
        private Product _testProduct;
        private BackendProduct _btestProduct;
        private BackendProductCategory _testCategory;


        [Test]
        public void ProductXMLParser_InsertProduct_ExpectCallToProtocol()
        {
            _uut.ProductXMLParser(_testProduct);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void ProductXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.ProductXMLParser(_testProduct);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void CategoryXMLParserr_InsertProduct_ExpectCallToProtocol()
        {
            _uut.CategoryXMLParser(_testCategory);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void CategoryXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.CategoryXMLParser(_testCategory);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void EditCategoryXMLParserr_InsertProduct_ExpectCallToProtocol()
        {
            _uut.EditCategoryXMLParser(_testCategory);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void EditCategoryXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.EditCategoryXMLParser(_testCategory);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void EditProductXMLParser_InsertProduct_ExpectCallToProtocol()
        {
            _uut.EditProductXMLParser(_btestProduct);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void EditProductXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.EditProductXMLParser(_btestProduct);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void DeleteProductXMLParser_InsertProduct_ExpectCallToProtocol()
        {
            _uut.DeleteProductXMLParser(_btestProduct);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void DeleteProductXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.DeleteProductXMLParser(_btestProduct);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void DeleteCategoryXMLParserr_InsertProduct_ExpectCallToProtocol()
        {
            _uut.DeleteCategoryXMLParser(_testCategory);
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void DeleteCategoryXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.DeleteCategoryXMLParser(_testCategory);
            Assert.AreEqual(totest, "TEST");
        }

        [Test]
        public void GetCatalogueXMLParserr_InsertProduct_ExpectCallToProtocol()
        {
            _uut.GetCatalougXMLParser();
            _protocol.Received(1).Encode(Arg.Any<Command>());
        }

        [Test]
        public void GetCatalogueXMLParser_InsertProduct_ExpectReturnTest()
        {
            var totest = _uut.GetCatalougXMLParser();
            Assert.AreEqual(totest, "TEST");
        }
    }
}