using System;
using System.Collections.Generic;
using Backend.AddProduct;
using Backend.Communication;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using Product = SharedLib.Models.Product;

namespace Backend.Unit.Tests
{
    [TestFixture]
    public class AddProductCBUnitTests
    {
        private IProductGenerator productGenerator;
        private IClient client;
        private IProtocol protokol;
        private AddProductCB uut;

        [SetUp]
        public void Setup()
        {
            productGenerator = Substitute.For<IProductGenerator>();
            client = Substitute.For<IClient>();
            protokol = Substitute.For<IProtocol>();
            uut = new AddProductCB(protokol,client);
        }



        [Test]
        public void CreateProduct_GoodData_ExpectCallToProtocol()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "Test",
                ["PRICE"] = "100",
                ["BARCODE"] = "TEST100"
            };

            uut.CreateProduct(fakedata);
            protokol.Received(1).ProductXMLParser(Arg.Any<Product>());

        }

        [Test]
        public void CreateProduct_ClientReturnsFalse_ExpectError()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "Test",
                ["PRICE"] = "100",
                ["BARCODE"] = "TEST100"
            };
            client.Send(Arg.Any<string>()).Returns(false);

            Assert.False(uut.CreateProduct(fakedata));

        }

        [Test]
        public void CreateProduct_GoodData_ExpectCallToClient()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "Test",
                ["PRICE"] = "100",
                ["BARCODE"] = "TEST100"
            };
            client.Send(Arg.Any<string>()).Returns(true);

            uut.CreateProduct(fakedata);
            client.Received(1).Send(Arg.Any<string>());

        }

        [Test]
        public void CreateProduct_BadPrice_ExpectError()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "Test",
                ["PRICE"] = "-5",
                ["BARCODE"] = "TEST100"
            };

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError,Is.EqualTo("Enter correct product details."));
        }

        [Test]
        public void CreateProduct_BadName_ExpectError()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "",
                ["PRICE"] = "110",
                ["BARCODE"] = "TEST100"
            };

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError, Is.EqualTo("Enter correct product details."));
        }

        [Test]
        public void CreateProduct_Badbarcode_ExpectError()
        {

            var fakedata = new Dictionary<string, string>
            {
                ["NAME"] = "Test",
                ["PRICE"] = "110",
                ["BARCODE"] = ""
            };

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError, Is.EqualTo("Enter correct product details."));
        }




    }

}