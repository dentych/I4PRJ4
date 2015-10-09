using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Brains;
using Backend.Models;
using NUnit.Framework;
using Backend.ViewModels;
using NSubstitute;

namespace Backend.Unit.Tests
{
    [TestFixture]
    class AddProductVmUnittest
    {
        private AddProductViewModel _uut;
        private BackendProduct _product;
        private IAddProduct _IAP;
        private IError _Err;

        [SetUp]
        public void Setup()
        {
            _uut = new AddProductViewModel();
            _product = new BackendProduct();
            _IAP = Substitute.For<IAddProduct>();
            _Err = Substitute.For<IError>();
        }

        [Test]
        public void Valid_NotValidProductName_ExpectFalse()
        {
            _product.BName = "";
            _product.BPrice = 10;
            _product.BProductNumber = "102D";
            _uut.Product = _product;

            Assert.False(_uut.Valid());
        }

        [Test]
        public void Valid_NotValidProductPrice_ExpectFalse()
        {
            _product.BName = "HAY";
            _product.BPrice = -5;
            _product.BProductNumber = "102D";
            _uut.Product = _product;

            Assert.False(_uut.Valid());
        }

        [Test]
        public void Valid_NotValidProductNumber_ExpectFalse()
        {
            _product.BName = "HAT";
            _product.BPrice = 10;
            _product.BProductNumber = "";
            _uut.Product = _product;

            Assert.False(_uut.Valid());
        }

        [Test]
        public void Valid_ValidProduct_ExpectTrue()
        {
            _product.BName = "HAT";
            _product.BPrice = 10;
            _product.BProductNumber = "HAT10";
            _uut.Product = _product;

            Assert.True(_uut.Valid());
        }

        [Test]
        public void AddProductCommand_Execute_ExpectCallToAddProduct()
        {
            _product.BName = "HAT";
            _product.BPrice = 10;
            _product.BProductNumber = "HAT10";
            _uut.Product = _product;

            _uut.IAP = _IAP;
            _uut.AddProductCommand.Execute(_product);

            _IAP.Received(1).CreateProduct(_product);
        }

        [Test]
        public void Constructor_NoValuesErr_ExpectStandardValues()
        {
            Assert.That(_uut.Err, Is.TypeOf(typeof(Error)));
        }

        [Test]
        public void Constructor_NoValuesProduct_ExpectStandardValues()
        {
            Assert.That(_uut.Product, Is.TypeOf(typeof(BackendProduct)));
        }

        [Test]
        public void Constructor_NoValuesIAP_ExpectStandardValues()
        {
            Assert.That(_uut.IAP, Is.TypeOf(typeof(AddProductCB)));
        }



    }
}
