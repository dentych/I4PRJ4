using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Brains;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace Backend.Unit.Tests.ViewModels
{
    [TestFixture]
    public class EditCategoryViewModelUnitTests
    {
        private EditCategoryViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new EditCategoryViewModel();
            vm.AllCats = new BackendProductCategoryList();
        }

        [Test]
        public void SetCategoryData_UpdatesProperties()
        {
            var cats = new BackendProductCategoryList();
            vm.SetCategoryData(new EditCategoryParms
            {
                Name = "Kategori",
                cats = cats,
                Id = 1
            });

            Assert.AreEqual(vm.ProductCategoryEdited.Name, "Kategori");
            Assert.AreEqual(vm.ProductCategoryEdited.ProductCategoryId, 1);
            Assert.AreEqual(vm.AllCats, cats);
        }

        [Test]
        public void SaveCommand_CategoryNameEmpty_CanNotExecute()
        {
            vm.ProductCategoryEdited.Name = "";

            Assert.IsFalse(vm.SaveCategoryCommand.CanExecute(null));
        }

        [Test]
        public void SaveCommand_CategoryNameFilled_CanExecute()
        {
            vm.ProductCategoryEdited.Name = "Kategori";

            Assert.IsTrue(vm.SaveCategoryCommand.CanExecute(null));
        }

        [Test]
        public void SaveCommand_NewCategoryValidName_ModelHandlerCalled()
        {
            vm.Handler = Substitute.For<IModelHandler>();
            vm.AllCats.Add(new BackendProductCategory
            {
                BName = "RandomKategori"
            });
            vm.ProductCategoryEdited.Name = "Kategori";

            vm.SaveCategoryCommand.Execute(null);

            vm.Handler.Received(1).EditCategory(Arg.Any<BackendProductCategory>());
        }

        [Test]
        public void SaveCommand_CategoryExists_ErrorShown()
        {
            vm.Err = Substitute.For<IError>();
            vm.AllCats.Add(new BackendProductCategory
            {
                BName = "Kategori"
            });

            vm.ProductCategoryEdited.BName = "Kategori";

            vm.SaveCategoryCommand.Execute(null);

            vm.Err.Received(1).StdErr(Arg.Any<string>());
        }
    }
}
