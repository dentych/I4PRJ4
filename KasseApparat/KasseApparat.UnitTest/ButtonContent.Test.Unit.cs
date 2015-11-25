using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SharedLib.Models;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class ButtonContentTest
    {
        private ButtonContent _uut;
        private Product _prod;
        private ShoppingList sl;

        [Test]
        public void CreateButton_CreateButtonWithStrings_CorrectNameReturned()
        {
            _uut = new ButtonContent("Kage", "20");

            Assert.That(_uut.Name, Is.EqualTo("Kage"));
        }

        [Test]
        public void CreateButton_CreateButtonWithStrings_CorrectPriceReturned()
        {
            _uut = new ButtonContent("Kage", "20");

            Assert.That(_uut.Price, Is.EqualTo("20 kr."));
        }

        [Test]
        public void CreateButton_CreateButtonWithEmptyStrings_CorrectNameReturned()
        {
            _uut = new ButtonContent("", "");

            Assert.That(_uut.Price, Is.EqualTo(""));
        }

        [Test]
        public void CreateButton_CreateButtonWithEmptyStrings_CorrectPriceReturned()
        {
            _uut = new ButtonContent("", "");

            Assert.That(_uut.Price, Is.EqualTo(""));
        }

        [Test]
        public void CreateButton_CreateButtonWithStrings_CorrectPricesReturned()
        {
            _uut = new ButtonContent("Kage", "20");

            Assert.That(_uut.Price, Is.EqualTo("20 kr."));
        }

        [Test]
        public void CreateButton_CreateButtonWithProduct_CorrectProductReturned()
        {
            _prod = new Product
            {
                Name = "Kage",
                Price = 20
            };

            sl = Substitute.For<ShoppingList>();

            _uut = new ButtonContent(_prod, sl);

            Assert.That(_uut.Product, Is.EqualTo(_prod));
        }



        [Test]
        public void CreateButton_CreateButtonWithProduct_CorrectNameReturned()
        {
            _prod = new Product
            {
                Name = "Kage",
                Price = 20
            };

            sl = Substitute.For<ShoppingList>();

            _uut = new ButtonContent(_prod, sl);

            Assert.That(_uut.Name, Is.EqualTo(_prod.Name));
        }

        [Test]
        public void CreateButton_CreateButtonWithProduct_CorrectPriceReturned()
        {
            _prod = new Product
            {
                Name = "Kage",
                Price = 20
            };

            sl = Substitute.For<ShoppingList>();

            _uut = new ButtonContent(_prod, sl);

            Assert.That(_uut.Price, Is.EqualTo(_prod.Price + " kr."));
        }

        [Test]
        public void CreateButton_CreateButtonWithEmptyProduct_CorrectNameReturned()
        {
            _prod = new Product();

            sl = Substitute.For<ShoppingList>();

            _uut = new ButtonContent(_prod, sl);

            Assert.That(_uut.Name, Is.EqualTo(""));
        }

        [Test]
        public void CreateButton_CreateButtonWithEmptyProduct_CorrectPriceReturned()
        {
            _prod = new Product();

            sl = Substitute.For<ShoppingList>();

            _uut = new ButtonContent(_prod, sl);

            Assert.That(_uut.Price, Is.EqualTo(""));
        }


    }
}
