using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        [Test]
        public void AddItem_Add3rdItem_ExpectJuice()
        {
            var prod = new Product();
            prod.Name = "Juice";

            uut.AddItem(new PurchasedProduct(prod, 5));

            Assert.That(uut[2].Name, Is.EqualTo("Juice"));
        }

        [Test]
        public void IncrementQuantity_Add1_Expect6()
        {
            uut.IncrementQuantity(0);

            Assert.That(uut[0].Quantity, Is.EqualTo(6));
        }
    }
}
