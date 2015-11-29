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
    public class DeleteCategoryTests
    {
        private DeleteCategoryViewModel _uut;
        private IClient fakeClient;

        [SetUp]
        public void Setup()
        {
            _uut = new DeleteCategoryViewModel();
            _uut.Categories = new BackendProductCategoryList();
            _uut.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 0,
                BName = "Kategori1",
                Products = new List<Product>()
                {
                    new BackendProduct()
                    {
                        Name = "Test",
                        Price = 100,
                        ProductNumber = "123",
                        ProductCategoryId = 0
                    }
                }
            });
            _uut.Categories.Add(new BackendProductCategory
            {
                ProductCategoryId = 1,
                BName = "Kategori2",
                Products = new List<Product>()
            });
            fakeClient = Substitute.For<IClient>();
            var mh = new ModelHandler(new PrjProtokol(), fakeClient);


            _uut.ModelHandler = mh;


        }

        [Test]
        public void MoveCategory_Kategori_ExpectCallToClientWith1Move()
        {

            _uut.MoveToCategoryId = 1;
            _uut.SelectedIndex = 0;
            _uut.MoveCommand.Execute(null);

            string toxpect =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"EditProduct\"><Product Name=\"Test\" ProductNumber=\"123\" Price=\"100\" ProductId=\"0\" ProductCategoryId=\"1\" /></Command>";

            fakeClient.Received(1).Send(toxpect);

        }
        [Test]

        public void DeleteCategory_Kategori_ExpectCallToClientWith1Delete()
        {

            _uut.SelectedIndex = 0;
            _uut.DeleteCommand.Execute(null);

            string toxpect =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"DeleteProductCategory\"><ProductCategory Name=\"Kategori1\" ProductCategoryId=\"0\" /></Command>";

            fakeClient.Received(1).Send(toxpect);

        }
    }
}
