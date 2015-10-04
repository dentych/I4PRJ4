using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharedLib.Models;

namespace KasseApparat.UnitTest
{
    [TestFixture]
    class ShoppingListTest
    {
        private ShoppingList uut = null;

        [SetUp]
        public void Setup()
        {
            uut = new ShoppingList();
            uut.Clear();

            var prod1 = new Product();
            prod1.Price = 10;
            var prod2 = new Product();
            prod2.Price = 5;

            uut.Add(new PurchasedProduct(prod1, 5));
            uut.Add(new PurchasedProduct(prod2, 4));
        }

        [Test]
        public void TotalPrice_GetTotalPrice_Expect70()
        {
            Assert.That(uut.TotalPrice, Is.EqualTo(70));
        }

        [Test]
        public void Quantity_ChangeQuantity_Expect6()
        {
            uut.CurrentIndex = 0;
            uut[uut.CurrentIndex].Quantity++;

            Assert.That(uut[uut.CurrentIndex].Quantity, Is.EqualTo(6));
        }
    }
}
