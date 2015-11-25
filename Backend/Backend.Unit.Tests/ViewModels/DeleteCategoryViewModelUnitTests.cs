using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Brains;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;
using SharedLib.Models;

namespace Backend.Unit.Tests.ViewModels
{
    [TestFixture]
    class DeleteCategoryViewModelUnitTests
    {
        private DeleteCategoryViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new DeleteCategoryViewModel();
            vm.Categories = new BackendProductCategoryList();
        }

        [Test]
        public void GetData_SetsCategories()
        {
            var catlist = new BackendProductCategoryList();
            var deleteindex = 1;

            var param = new DeleteCategoryParms
            {
                cats = catlist,
                ToDelteIndex = deleteindex
            };
            vm.GetData(param);

            Assert.AreEqual(vm.Categories, catlist);
        }

        [Test]
        public void MoveCommand_SelectedIndexAndMoveToCategoryIdSame_CanNotExecute()
        {
            vm.Categories.Add(new BackendProductCategory());
            vm.Categories[0].AddProduct(new BackendProduct());
            vm.MoveToCategoryId = 0;
            vm.GetData(new DeleteCategoryParms
            {
                cats = vm.Categories,
                ToDelteIndex = 0

            });

            Assert.IsFalse(vm.MoveCommand.CanExecute(null));
        }

        [Test]
        public void MoveCommand_CategoryProductListEmpty_CanNotExecute()
        {
            vm.Categories.Add(new BackendProductCategory());
            vm.MoveToCategoryId = 0;
            vm.GetData(new DeleteCategoryParms
            {
                cats = vm.Categories,
                ToDelteIndex = 0

            });

            Assert.IsFalse(vm.MoveCommand.CanExecute(null));
        }

        [Test]
        public void MoveCommand_ValidMove_CanExecute()
        {
            vm.Categories.Add(new BackendProductCategory());
            vm.Categories[0].AddProduct(new BackendProduct());
            vm.MoveToCategoryId = 1;
            vm.GetData(new DeleteCategoryParms
            {
                cats = vm.Categories,
                ToDelteIndex = 0
            });

            Assert.IsTrue(vm.MoveCommand.CanExecute(null));
        }

        [Test]
        public void MoveCommand_Execution_ModelHandlerCalled()
        {
            vm.ModelHandler = Substitute.For<IModelHandler>();
            vm.Categories.Add(new BackendProductCategory());
            vm.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1,
            });
            vm.Categories[0].AddProduct(new BackendProduct());
            vm.MoveToCategoryId = 1;
            vm.GetData(new DeleteCategoryParms
            {
                cats = vm.Categories,
                ToDelteIndex = 0
            });

            vm.MoveCommand.Execute(null);

            vm.ModelHandler.Received(1).MoveProductsInCategory(
                Arg.Any<BackendProductCategory>(), Arg.Any<int>());
        }

        [Test]
        public void DeleteCommand_CurrentProductListNotEmpty_CanNotExecute()
        {
            vm.Categories.CurrentProductList = new List<Product>();
            vm.Categories.CurrentProductList.Add(new BackendProduct());
            vm.Categories.CurrentProductList.Add(new BackendProduct());
            vm.Categories.CurrentProductList.Add(new BackendProduct());

            Assert.IsFalse(vm.DeleteCommand.CanExecute(null));
        }

        [Test]
        public void DeleteCommand_CurrentProductListEmpty_CanExecute()
        {
            vm.Categories.CurrentProductList = new List<Product>();
            
            Assert.IsTrue(vm.DeleteCommand.CanExecute(null));
        }

        [Test]
        public void DeleteCommand_ValidDelete_ModelHandlerCalled()
        {
            vm.ModelHandler = Substitute.For<IModelHandler>();
            vm.Categories.Add(new BackendProductCategory());
            vm.Categories.CurrentProductList = vm.Categories[0].Products;
            vm.GetData(new DeleteCategoryParms
            {
                cats = vm.Categories,
                ToDelteIndex = 0
            });

            vm.DeleteCommand.Execute(null);

            vm.ModelHandler.Received(1).DeleteCategory(Arg.Any<BackendProductCategory>());
        }
    }
}
