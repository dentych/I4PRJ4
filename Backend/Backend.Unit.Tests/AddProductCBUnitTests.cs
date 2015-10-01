using Backend.AddProduct;
using Backend.Communication;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Unit.Tests
{
    [TestFixture]
    public class AddProductCBUnitTests
    {
        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IClient>();
            _protocol = Substitute.For<IProtocol>();
            _generator = Substitute.For<IProductGenerator>();


            _uut = new AddProductCB(_protocol, Window, _client);
            _uut._productGenerator = _generator;
        }

        private IClient _client;
        private IProtocol _protocol;
        private IProductGenerator _generator;

        private AddProductCB _uut;

        public AddProductWindow Window { get; set; }

        [Test]
        public void CreateProduct_BadProduct_ExpectFalseAndLastError()
        {
            var fakeProduct = new Product();
            fakeProduct.Price = -150;
            _generator.GenerateProduct().Returns(fakeProduct);
            _uut.CreateProduct();
            Assert.That(_uut.LastError, Is.EqualTo("Enter correct product details."));
        }


        [Test]
        public void CreateProduct_CorrectData_expectCallToClient()
        {
            var fakeProduct = new Product();
            fakeProduct.Name = "Test";
            fakeProduct.Price = 5;
            fakeProduct.ProductNumber = "1234";


            _generator.GenerateProduct().Returns(fakeProduct);
            _protocol.ProductXMLParser(fakeProduct).Returns("TestXML");
            _uut.CreateProduct();
            _client.Received().Send("TestXML");
        }

        [Test]
        public void CreateProduct_CorrectData_ExpectTrue()
        {
            var fakeProduct = new Product();
            fakeProduct.Name = "Test";
            fakeProduct.Price = 5;
            fakeProduct.ProductNumber = "1234";


            _generator.GenerateProduct().Returns(fakeProduct);
            _protocol.ProductXMLParser(fakeProduct).Returns("TestXML");
            _client.Send("TestXML").Returns(true);
            Assert.True(_uut.CreateProduct());
        }

        [Test]
        public void CreateProduct_WithNullProduct_expectFalse()
        {
            var fakeProduct = new Product();
            _generator.GenerateProduct().Returns(fakeProduct);
            Assert.False(_uut.CreateProduct());
        }
    }

    [TestFixture]
    public class GenerateProductTests
    {
        // DEN ER PROBLEMATISK....
    }
}