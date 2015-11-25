using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class ProductButtonControlTest
    {
        private ProductButtonControl _uut;


        [SetUp]
        public void SetUp_function()
        {
            //FakeDBcontrol dbc = new FakeDBcontrol();
            //_uut = new ProductButtonControl(dbc.GetProducts()[0].Products.ToList());
        }

        [Test]
        public void CreatePBC_CreateWithoutList_PBCCreatedSuccesfully()
        {
            _uut = new ProductButtonControl();
        }

        public void CreateList_createListWithoutProducts_ListCreated()
        {
            _uut = new ProductButtonControl();

            Assert.That(_uut.CurrentButtonPage, Is.EqualTo(null));
        }

        [Test]
        public void CreateList_createListWithProducts_ListCreated()
        {
            FakeDBcontrol dbc = new FakeDBcontrol();
            _uut = new ProductButtonControl(dbc.GetProducts()[0].Products.ToList());

            Assert.That(_uut.CurrentButtonPage[0].Name, Is.EqualTo("Beer"));
        }

        [Test]
        public void TotalPages_CalculateTotalPages_CorrectPage()
        {
            FakeDBcontrol dbc = new FakeDBcontrol();
            _uut = new ProductButtonControl(dbc.GetProducts()[1].Products.ToList());

            _uut.CalculateTotalpage();

            Assert.That(_uut.TotalPages, Is.EqualTo(2));
        }

        [Test]
        public void TotalPages_CalculateTotalPagesWithoutList_CorrectPageNumber()
        {
            _uut = new ProductButtonControl();

            _uut.CalculateTotalpage();

            Assert.That(_uut.TotalPages, Is.EqualTo(0));
        }

        [Test]
        public void CurrentPages_CreateListAndCheckCurrentPage_CurrentPageIs1()
        {
            _uut = new ProductButtonControl();

            Assert.That(_uut.CurrentPages, Is.EqualTo(1));
        }

        [Test]
        public void Update_CallUpdate_CheckIfUpdateWentThrough()
        {
            FakeDBcontrol dbc = new FakeDBcontrol();
            _uut = new ProductButtonControl(dbc.GetProducts()[0].Products.ToList());

            _uut.Update(dbc.GetProducts()[1].Products.ToList());

            Assert.That(_uut.TotalPages, Is.EqualTo(2));
        }
    }
}
