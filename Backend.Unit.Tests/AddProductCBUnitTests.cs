using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Backend.AddProduct;
using Backend.Communication;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace Backend.Unit.Tests
{
    [TestFixture]
    public class AddProductCBUnitTests
    {
        private IClient _client;
        private IProtocol _protocol;
        private IProductGenerator _generator;
        private AddProductWindow _window;

        private AddProductCB _uut;

        public AddProductWindow Window
        {
            get
            {
                return _window;
            }

            set
            {
                _window = value;
            }
        }

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IClient>();
            _protocol = Substitute.For<IProtocol>();
            _generator = Substitute.For<IProductGenerator>();


            _uut = new AddProductCB(_protocol, Window, _client);
            _uut._productGenerator = _generator;


        }


        [Test]
        public void CreateProduct_CorrectData_expectCallToClient()
        {
            Product fakeProduct = new Product();
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
            Product fakeProduct = new Product();
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

            Product fakeProduct = new Product();
            _generator.GenerateProduct().Returns(fakeProduct);
            Assert.False(_uut.CreateProduct());
        }

        [Test]
        public void CreateProduct_BadProduct_ExpectFalseAndLastError()
        {
            Product fakeProduct = new Product();
            fakeProduct.Price = -150;
            _generator.GenerateProduct().Returns(fakeProduct);
            _uut.CreateProduct();
            Assert.That(_uut.LastError, Is.EqualTo("Enter correct product details."));

        }


    }

    [TestFixture]
    public class GenerateProductTests
    {
        // DEN ER PROBLEMATISK....
    }













}
