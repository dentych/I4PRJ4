using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace Backend.Tests.Integration
{
    [TestFixture]
    public class AddCategoryTests
    {
        [SetUp]
        public void Setup()
        {
            _uut = new AddCategoryViewModel();
            _uut.Categories = new BackendProductCategoryList();
            fakeClient = Substitute.For<IClient>();
            var mh = new ModelHandler(new PrjProtokol(), fakeClient);


            _uut.Handler = mh;
        }

        private AddCategoryViewModel _uut;
        private IClient fakeClient;

        [Test]
        public void AddCategory_NewKategori_ExpectCallToClient()
        {
            _uut.Category = new BackendProductCategory
            {
                Name = "TEST"
            };

            _uut.AddCategoryCommand.Execute(null);

            string toxpect =
                "<?xml version=\"1.0\" encoding=\"utf-16\"?><Command Name=\"CreateProductCategory\"><ProductCategory Name=\"TEST\"><ProductList /></ProductCategory></Command>";
            fakeClient.Received(1).Send(toxpect);
        }

        [Test]
        public void AddCategory_NewKategoriAgain_ExpectNoCallToClientButCallToErrorPRinter()
        {
            _uut.ErrorPrinter = Substitute.For<IError>();
            _uut.Categories.Add(new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 0
            });

            _uut.Category = new BackendProductCategory
            {
                Name = "TEST",
                ProductCategoryId = 0

            };

            _uut.AddCategoryCommand.Execute(null);
            fakeClient.DidNotReceive().Send(Arg.Any<string>());
            _uut.ErrorPrinter.Received(1).StdErr("Kategorien eksisterer allerede.");
        }
    }
}