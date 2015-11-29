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
    class EditProductTests
    {
        private EditProductViewModel _uut;
        private IClient _fakeClient;

        [SetUp]
        public void Setup()
        {
            _uut = new EditProductViewModel();
            _uut.Categories = new BackendProductCategoryList();
            _uut.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1,
                BName = "Kategori",
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        ProductCategoryId = 1,
                        ProductNumber = "123",
                        ProductId = 1
                    }
                }
            });

            _fakeClient = Substitute.For<IClient>();
            _uut.Handler = new ModelHandler(new PrjProtokol(), _fakeClient);
        }

        [Test]
        public void AddProduct_NewProduct_ExpectCallToClient()
        {
            _uut.EditedProduct = new BackendProduct()
            {
                Name = "Test2",
                ProductCategoryId = 1,
                ProductNumber = "123",
                ProductId = 1
            };

            _uut.SaveProductCommand.Execute(null);
            string toxpect =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"EditProduct\"><Product Name=\"Test2\" ProductNumber=\"123\" Price=\"0\" ProductId=\"1\" ProductCategoryId=\"1\" /></Command>";
            _fakeClient.Received(1).Send(toxpect);

        }
    }
}
