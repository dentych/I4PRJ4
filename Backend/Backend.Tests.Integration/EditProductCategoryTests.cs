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
    public class EditProductCategoryTests
    {

        private EditCategoryViewModel _uut;
        private IClient _fakeClient;

        [SetUp]
        public void SetUp()
        {
            _uut = new EditCategoryViewModel();

            _uut.AllCats = new BackendProductCategoryList()
            {
                new BackendProductCategory()
                {
                    Name = "Test",
                    ProductCategoryId = 0,
                    Products = new List<Product>()
                }
            };
            _fakeClient = Substitute.For<IClient>();
            var mh = new ModelHandler(new PrjProtokol(), _fakeClient);


            _uut.Handler = mh;

        }

        [Test]
        public void EditCategory_EditName_ExpectCallToClient()
        {
            _uut.ProductCategoryEdited = new BackendProductCategory()
            {
                Name = "Test2",
                ProductCategoryId = 0,
                Products = new List<Product>()
            };

            _uut.SaveCategoryCommand.Execute(null);
            string toxpect =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"EditProductCategory\"><ProductCategory Name=\"Test2\" ProductCategoryId=\"0\"><ProductList /></ProductCategory></Command>";
            _fakeClient.Received(1).Send(toxpect);

        }
    }
}
