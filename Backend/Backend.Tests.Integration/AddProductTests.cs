using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Tests.Integration
{
    [TestFixture]
    class AddProductTests
    {
        private AddProductViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new AddProductViewModel();
            vm.Categories = new BackendProductCategoryList();
            vm.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1,
                BName = "Kategori"
            });
            vm.SelectedCategory = vm.Categories[0];
        }

        [Test]
        public void ValidProductClientCalled()
        {
            var client = Substitute.For<IClient>();
            vm.ModelHandler = new ModelHandler(new PrjProtokol(), client);
            vm.Product.BName = "Produkt123";
            vm.Product.BPrice = 1231234;
            vm.Product.BProductNumber = "Barcode123";
            vm.Product.ProductCategoryId = 1;

            vm.AddProductCommand.Execute(null);

            client.Received(1).Send(Arg.Is<string>(x =>
                x.Contains("Produkt123") &&
                x.Contains("Barcode123") &&
                x.Contains("1231234")
                ));
        }

        [Test]
        public void ValidProductProtocolReceivedCorrectName()
        {
            var protocol = Substitute.For<IBProtocol>();
            var client = Substitute.For<IClient>();
            vm.ModelHandler = new ModelHandler(protocol, client);
            vm.Product.BName = "Produkt123";
            vm.Product.BPrice = 1231234;
            vm.Product.BProductNumber = "Barcode123";
            vm.Product.ProductCategoryId = 1;

            vm.AddProductCommand.Execute(null);

            protocol.Received(1).ProductXMLParser(Arg.Is<Product>(x =>
                x.Name == "Produkt123"));
        }

        [Test]
        public void ValidProductProtocolReceivedCorrectPrice()
        {
            var protocol = Substitute.For<IBProtocol>();
            var client = Substitute.For<IClient>();
            vm.ModelHandler = new ModelHandler(protocol, client);
            vm.Product.BName = "Produkt123";
            vm.Product.BPrice = 1231234;
            vm.Product.BProductNumber = "Barcode123";
            vm.Product.ProductCategoryId = 1;

            vm.AddProductCommand.Execute(null);

            protocol.Received(1).ProductXMLParser(Arg.Is<Product>(x =>
                x.Price == 1231234));
        }

        [Test]
        public void ValidProductProtocolReceivedCorrectCategoryId()
        {
            var protocol = Substitute.For<IBProtocol>();
            var client = Substitute.For<IClient>();
            vm.ModelHandler = new ModelHandler(protocol, client);
            vm.Product.BName = "Produkt123";
            vm.Product.BPrice = 1231234;
            vm.Product.BProductNumber = "Barcode123";

            vm.AddProductCommand.Execute(null);

            protocol.Received(1).ProductXMLParser(Arg.Is<Product>(x =>
                x.ProductCategoryId == 1));
        }
    }
}
