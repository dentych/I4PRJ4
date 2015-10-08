using System;
using System.Collections.Generic;
using Backend.AddProduct;
using Backend.AddProduct.Brains;
using Backend.AddProduct.Models;
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

        private IClient client;
        private IProtocol protokol;
        private AddProductCB uut;

        [SetUp]
        public void Setup()
        {
            client = Substitute.For<IClient>();
            protokol = Substitute.For<IProtocol>();
            uut = new AddProductCB(protokol,client);
        }



        [Test]
        public void CreateProduct_GoodData_ExpectCallToProtocol()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            uut.CreateProduct(fakedata);
            protokol.Received(1).ProductXMLParser(Arg.Any<Product>());

        }

        [Test]
        public void CreateProduct_ClientReturnsFalse_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";
            client.Send(Arg.Any<string>()).Returns(false);

            Assert.False(uut.CreateProduct(fakedata));

        }

        [Test]
        public void CreateProduct_GoodData_ExpectCallToClient()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            client.Send(Arg.Any<string>()).Returns(true);

            uut.CreateProduct(fakedata);
            client.Received(1).Send(Arg.Any<string>());

        }

        [Test]
        public void CreateProduct_BadPrice_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = -5;
            fakedata.BProductNumber = "1124TEST";

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError,Is.EqualTo("Enter correct product details."));
        }

        [Test]
        public void CreateProduct_BadName_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError, Is.EqualTo("Enter correct product details."));
        }

        [Test]
        public void CreateProduct_Badbarcode_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "";

            uut.CreateProduct(fakedata);
            Assert.That(uut.LastError, Is.EqualTo("Enter correct product details."));
        }




    }
    
}