using Backend.Models.Brains;
using Backend.Models.Datamodels;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;
using System.Collections.Generic;

namespace Backend.Unit.Tests.ViewModels
{
    [TestFixture]
    public class AddProductViewModelUnitTests
    {
        AddProductViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new AddProductViewModel();
            vm.Categories = new BackendProductCategoryList();
        }

        [Test]
        public void Valid_InputValid_ReturnsTrue()
        {
            vm.Product.BName = "TestProdukt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";

            Assert.IsTrue(vm.Valid());
        }

        [Test]
        public void Valid_BNameNull_ReturnsFalse()
        {
            vm.Product.BName = null;
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_BNameEmpty_ReturnsFalse()
        {
            vm.Product.BName = "";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_BNameWhitespacesOnly_ReturnsFalse()
        {
            vm.Product.BName = "        ";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";
        }

        [Test]
        public void Valid_BPriceBelowZero_ReturnsFalse()
        {
            vm.Product.BName = "Hej";
            vm.Product.BPrice = -1;
            vm.Product.BProductNumber = "Barcode";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_BProductNumberNull_ReturnsFalse()
        {
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = null;

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_BProductNumberEmpty_ReturnsFalse()
        {
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_BProductNumberWhitespacesOnly_ReturnsFalse()
        {
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "      ";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void CategoryListUpdated_CategoriesSet()
        {
            var catlist = new BackendProductCategoryList();
            vm.CategoryListUpdated(catlist);

            Assert.AreEqual(vm.Categories, catlist);
        }

        [Test]
        public void CategoryListUpdated_CategoryCountZero_SelectedCategoryNotSet()
        {
            vm.SelectedCategory = null;

            var catlist = new BackendProductCategoryList();
            vm.CategoryListUpdated(catlist);

            Assert.IsNull(vm.SelectedCategory);
        }

        [Test]
        public void CategoryListUpdated_CategoryCountAboveZero_SelectedCategorySet()
        {
            var catlist = new BackendProductCategoryList();
            var category = new BackendProductCategory();
            catlist.Add(category);
            vm.CategoryListUpdated(catlist);

            Assert.AreEqual(vm.SelectedCategory, category);
        }

        [Test]
        public void AddProductCommand_ValidProduct_CanExecuteTrue()
        {
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";

            Assert.IsTrue(vm.AddProductCommand.CanExecute(null));
        }

        [Test]
        public void AddProductCommand_InvalidProduct_CanNotExecute()
        {
            vm.Product.BName = "";
            vm.Product.BPrice = -1;
            vm.Product.BProductNumber = "";

            Assert.IsFalse(vm.AddProductCommand.CanExecute(null));
        }

        [Test]
        public void AddProductCommand_ValidProductExecute_ModelHandlerCalled()
        {
            vm.ModelHandler = Substitute.For<IModelHandler>();
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";
            vm.SelectedCategory = new BackendProductCategory();

            vm.AddProductCommand.Execute(null);

            vm.ModelHandler.Received(1).CreateProduct(Arg.Any<BackendProduct>());
        }

        [Test]
        public void AddProductCommand_ProductExists_ErrorShown()
        {
            vm.Err = Substitute.For<IError>();
            var cat = new BackendProductCategory();
            cat.AddProduct(new Product
            {
                ProductId = 1,
                Name = "Produkt",
                Price = 100,
                ProductCategoryId = 1,
                ProductNumber = "Barcode"
            });
            vm.Categories.Add(cat);
            
            vm.Product.BName = "Produkt";
            vm.Product.BPrice = 100;
            vm.Product.BProductNumber = "Barcode";

            vm.AddProductCommand.Execute(null);

            vm.Err.Received(1).StdErr(Arg.Any<string>());
        }
    }
}
