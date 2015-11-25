using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.ViewModels;
using Backend.Models.Events;
using Backend.Models.Datamodels;
using SharedLib.Models;
using NSubstitute;
using Backend.Models.Brains;

namespace Backend.Unit.Tests.ViewModels
{
    [TestFixture]
    class EditProductVMUnitTests
    {
        EditProductViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new EditProductViewModel();
            vm.Categories = new BackendProductCategoryList();
            vm.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1
            });
        }

        [Test]
        public void ProductDataToEdit_UpdatesEditedProduct()
        {
            var par = new EditProductParameters
            {
                cats = new BackendProductCategoryList(),
                product = new Product
                {
                    Name = "Produkt",
                    Price = 100,
                    ProductId = 1,
                    ProductNumber = "Barcode",
                },
                currentCatIndex = 1,
                CurrentCategory = new BackendProductCategory()
            };

            vm.ProductDataToEdit(par);

            Assert.AreEqual(par.product.Name, vm.EditedProduct.BName);
            Assert.AreEqual(par.product.Price, vm.EditedProduct.BPrice);
            Assert.AreEqual(par.product.ProductId, vm.EditedProduct.ProductId);
            Assert.AreEqual(par.product.ProductNumber, vm.EditedProduct.BProductNumber);
            Assert.AreEqual(par.cats, vm.Categories);
            Assert.AreEqual(par.currentCatIndex, vm.currentCatIndex);
        }

        [Test]
        public void SaveProductCommand_NewValidProduct_ModelHandlerCalled()
        {
            vm.Handler = Substitute.For<IModelHandler>();

            vm.EditedProduct.BName = "Produkt";
            vm.EditedProduct.BPrice = 100;
            vm.EditedProduct.BProductNumber = "Barcode";

            vm.SaveProductCommand.Execute(null);

            vm.Handler.Received(1).EditProduct(Arg.Any<BackendProduct>());
        }

        [Test]
        public void SaveProductCommand_ProductNameNull_CanNotExecute()
        {
            SetupProduct();
            vm.EditedProduct.BName = null;

            Assert.IsFalse(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ProductNameEmpty_CanNotExecute()
        {
            SetupProduct();
            vm.EditedProduct.BName = "";

            Assert.IsFalse(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ProductPriceBelowZero_CanNotExecute()
        {
            SetupProduct();
            vm.EditedProduct.BPrice = -1;

            Assert.IsFalse(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ProductBarcodeNull_CanNotExecute()
        {
            SetupProduct();
            vm.EditedProduct.BProductNumber = null;

            Assert.IsFalse(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ProductBarcodeEmpty_CanNotExecute()
        {
            SetupProduct();
            vm.EditedProduct.BProductNumber = "";

            Assert.IsFalse(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ValidProduct_CanExecute()
        {
            SetupProduct();

            Assert.IsTrue(vm.SaveProductCommand.CanExecute(null));
        }

        [Test]
        public void SaveProductCommand_ProductExists_ErrorCalled()
        {
            vm.Err = Substitute.For<IError>();

            vm.Categories.Add(new BackendProductCategory());
            vm.Categories[0].AddProduct(new Product
            {
                ProductId = 2,
                Name = "Produkt",
                Price = 100,
                ProductNumber = "Barcode"
            });

            SetupProduct();

            vm.SaveProductCommand.Execute(null);

            vm.Err.Received(1).StdErr(Arg.Any<string>());
        }

        private void SetupProduct()
        {
            vm.EditedProduct.ProductId = 1;
            vm.EditedProduct.BName = "Produkt";
            vm.EditedProduct.BPrice = 100;
            vm.EditedProduct.BProductNumber = "Barcode";
        }
    }
}
