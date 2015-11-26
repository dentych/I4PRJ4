using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Datamodels;
using Backend.ViewModels;
using NUnit.Framework;

namespace Backend.Tests.Integration
{
    [TestFixture]
    class EditProductTests
    {
        private EditProductViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new EditProductViewModel();
            vm.Categories = new BackendProductCategoryList();
            vm.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1,
                BName = "Kategori"
            });
        }
    }
}
