using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Brains;
using Backend.Models.Datamodels;
using Backend.ViewModels;
using NSubstitute;

namespace Backend.Unit.Tests.ViewModels
{
    [TestFixture]
    class AddCategoryViewModelUnitTests
    {
        private AddCategoryViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new AddCategoryViewModel();
            vm.Categories = new BackendProductCategoryList();
        }

        [Test]
        public void Valid_CategoryNameNull_ReturnFalse()
        {
            vm.Category.BName = null;

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_CategoryNameEmpty_ReturnFalse()
        {
            vm.Category.BName = "";

            Assert.IsFalse(vm.Valid());
        }

        [Test]
        public void Valid_CategoryNameValid_ReturnsTrue()
        {
            vm.Category.BName = "Kategori";

            Assert.IsTrue(vm.Valid());
        }

        [Test]
        public void CategoryListUpdated_UpdateCategories()
        {
            var cat = new BackendProductCategoryList();

            vm.CategoryListUpdated(cat);

            Assert.AreEqual(vm.Categories, cat);
        }

        [Test]
        public void AddCategoryCommand_ValidCategoryName_ModelHandlerCalled()
        {
            vm.Handler = Substitute.For<IModelHandler>();
            vm.Category.BName = "Kategori";
            vm.AddCategoryCommand.Execute(null);

            vm.Handler.Received(1).AddCategory(Arg.Any<BackendProductCategory>());
        }

        [Test]
        public void AddCategoryCommand_InvalidName_CanNotExecute()
        {
            vm.Category.BName = "";

            Assert.IsFalse(vm.AddCategoryCommand.CanExecute(null));
        }

        [Test]
        public void AddCategoryCommand_ValidCategoryName_CanExecute()
        {
            vm.Category.BName = "Kategori";

            Assert.IsTrue(vm.AddCategoryCommand.CanExecute(null));
        }

        [Test]
        public void AddCategoryCommand_CategoryNameExists_ShowError()
        {
            vm.Handler = Substitute.For<IModelHandler>();
            vm.ErrorPrinter = Substitute.For<IError>();
            vm.Categories.Add(new BackendProductCategory
            {
                BName = "Kategori"
            });
            vm.Category.BName = "Kategori";

            vm.AddCategoryCommand.Execute(null);

            vm.ErrorPrinter.Received(1).StdErr(Arg.Any<string>());
        }
    }
}
