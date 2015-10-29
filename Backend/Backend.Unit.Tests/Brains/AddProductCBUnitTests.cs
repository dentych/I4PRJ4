using Backend.Brains;
using Backend.Communication;
using Backend.Models;
using NSubstitute;
using NUnit.Framework;
using Product = SharedLib.Models.Product;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class AddProductCBUnitTests
    {

        private IClient client;
        private IProtocol protokol;
        private AddProductCB uut;
        private IError err;

        [SetUp]
        public void Setup()
        {
            client = Substitute.For<IClient>();
            protokol = Substitute.For<IProtocol>();
            uut = new AddProductCB(protokol,client);
            err = Substitute.For<IError>();
            uut.Error = err;
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
        public void CreateProduct_ClientReturnsFalse_ExpectErrorMsg()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";
            client.Send(Arg.Any<string>()).Returns(false);

            uut.CreateProduct(fakedata);
            err.Received(1).StdErr("Conenction error");

        }

        [Test]
        public void CreateProduct_GoodData_ExpectCallToClient()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            client.Connect().Returns(true);
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
            err.Received(1).StdErr("Enter correct product details.");
        }

       [Test]
        public void CreateProduct_BadName_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            uut.CreateProduct(fakedata);
            err.Received(1).StdErr("Enter correct product details.");

        }

        [Test]
        public void CreateProduct_Badbarcode_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "";

            uut.CreateProduct(fakedata);
            err.Received(1).StdErr("Enter correct product details.");
        }

        [Test]
        public void CreateProduct_ClientReturnsFalse_ExpectFalse()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            client.Connect().Returns(true);
            client.Send(Arg.Any<string>()).Returns(false);

            Assert.False(uut.CreateProduct(fakedata));
        }
        /* 

        */
    }
    
}