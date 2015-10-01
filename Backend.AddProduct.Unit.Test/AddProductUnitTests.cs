using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Backend.Communication;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace Backend.AddProduct.Unit.Test
{
    [TestFixture]
    public class AddProductCBTest
    {
        private IClient _client;
        private IProtocol _protocol;
        private IProductGenerator _generator;
        private AddProductWindow _window;

        private AddProductCB _uut;
        

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IClient>();
            _protocol = Substitute.For<IProtocol>();
            _generator = Substitute.For<IProductGenerator>();
  

            _uut = new AddProductCB(_protocol, _window, _client);
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
            _generator.GenerateProduct().Returns(fakeProduct);
            _uut.CreateProduct();
            Assert.That(_uut.LastError, Is.EqualTo("Enter correct product details."));

        }


    }

    [TestFixture]
    public class GenerateProductTest
    {
       // DEN ER PROBLEMATISK....
    }

    [TestFixture]
    public class PrjProtocolTest
    {

        [Test]
        public void ProductXMLParser_RealProdcuct_ExpectCorrectString()
        {
            var testProduct = new Product();
            testProduct.Name = "Test";
            testProduct.Price = 10;
            testProduct.ProductNumber = "ABC123";

            var uut = new PrjProtokol();

            Assert.That(uut.ProductXMLParser(testProduct), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"CreateProduct\"><Product Name=\"Test\" ProductNumber=\"ABC123\" Price=\"10\" /></Command>"));



        }


    }

    [TestFixture]
    public class ClientTest
    {
        private Client _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new Client("93.184.216.34", 80);
        }
        [Test]
        public void IP_SetIp_ExpectIP()
        {
            Assert.That(_uut.Ip, Is.EqualTo("93.184.216.34"));
        }

        [Test]
        public void Port_SetPort_ExpectPort()
        {
            Assert.That(_uut.Port, Is.EqualTo(80));
        }

        [Test]
        public void Send_RealtTest_ExpectTrue()
        {
            Assert.True(_uut.Send("TEST"));
        }


        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Bad IP")]
        public void IP_SetBadIP_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            var uut = new Client("1222222.111.222.336.588.85.55.444", 9000);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Bad port")]
        public void IP_SetPortHigh_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            _uut = new Client("93.184.216.34", 900000);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Bad port")]
        public void IP_SetPortLow_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            _uut = new Client("93.184.216.34", -1);
        }

        [Test]
        public void LOL_SetPortLow_ExpectException() // I CANNOT MAKEN SHIT FAIL
        {
            _uut = new Client("93.184.216.34", 9000);
            Assert.False(_uut.Send("\n\r"));

        }


    }
}
