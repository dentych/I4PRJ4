using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using NSubstitute;
using NUnit.Framework;
using Product = SharedLib.Models.Product;

namespace Backend.Unit.Tests.Brains
{
    [TestFixture]
    public class ModelHandlerUnitTests
    {

        private IClient _client;
        private IBProtocol _protokol;
        private ModelHandler _uut;
        private IError _err;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<IClient>();
            _protokol = Substitute.For<IBProtocol>();
            _uut = new ModelHandler(_protokol, _client);
            _err = Substitute.For<IError>();
            _uut.Error = _err;
        }



        [Test]
        public void CreateProduct_GoodData_ExpectCallToProtocol()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            _uut.CreateProduct(fakedata);
            _protokol.Received(1).ProductXMLParser(Arg.Any<Product>());

        }

        //[Test]
        //public void CreateProduct_ClientReturnsFalse_ExpectError()
        //{

        //    var fakedata = new BackendProduct();
        //    fakedata.BName = "Name";
        //    fakedata.BPrice = 100;
        //    fakedata.BProductNumber = "1124TEST";
        //    _client.Send(Arg.Any<string>()).Returns(false);

        //    Assert.False(_uut.CreateProduct(fakedata));
        //}

        //[Test]
        //public void CreateProduct_ClientReturnsFalse_ExpectErrorMsg()
        //{

        //    var fakedata = new BackendProduct();
        //    fakedata.BName = "Name";
        //    fakedata.BPrice = 100;
        //    fakedata.BProductNumber = "1124TEST";
        //    _client.Send(Arg.Any<string>()).Returns(false);

        //    _uut.CreateProduct(fakedata);
        //    _err.Received(1).StdErr("Conenction error");

        //}

        [Test]
        public void CreateProduct_GoodData_ExpectCallToClient()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            _client.Connect().Returns(true);
            _client.Send(Arg.Any<string>()).Returns(true);

            _uut.CreateProduct(fakedata);
            _client.Received(1).Send(Arg.Any<string>());

        }

        [Test]
        public void CreateProduct_BadPrice_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = -5;
            fakedata.BProductNumber = "1124TEST";

            _uut.CreateProduct(fakedata);
            _err.Received(1).StdErr("Enter correct product details.");
        }

        [Test]
        public void CreateProduct_BadName_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "1124TEST";

            _uut.CreateProduct(fakedata);
            _err.Received(1).StdErr("Enter correct product details.");

        }

        [Test]
        public void CreateProduct_Badbarcode_ExpectError()
        {

            var fakedata = new BackendProduct();
            fakedata.BName = "Name";
            fakedata.BPrice = 100;
            fakedata.BProductNumber = "";

            _uut.CreateProduct(fakedata);
            _err.Received(1).StdErr("Enter correct product details.");
        }

        //[Test]
        //public void CreateProduct_ClientReturnsFalse_ExpectFalse()
        //{

        //    var fakedata = new BackendProduct();
        //    fakedata.BName = "Name";
        //    fakedata.BPrice = 100;
        //    fakedata.BProductNumber = "1124TEST";

        //    _client.Connect().Returns(true);
        //    _client.Send(Arg.Any<string>()).Returns(false);

        //    Assert.False(_uut.CreateProduct(fakedata));
        //}
        /* 

        */
    }

}