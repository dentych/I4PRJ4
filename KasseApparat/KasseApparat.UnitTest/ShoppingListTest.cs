using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Test]
        public void Notify_()
        {
            bool test = false;
            uut.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { test = true; };

            uut.AddItem(new PurchasedProduct(new Product(),1));

            Assert.True(test);
        }
    }

    [TestFixture]
    class ShoppingListTest_Commands
    {
        private ShoppingList uut = null;

        [SetUp]
        public void SetUp()
        {
            uut = new ShoppingList();
            uut.Clear();
        }

        //More -----------------------------------------------------------------------

        [Test]
        public void MoreCommand_CanExecute1_True()
        {
            uut.CurrentIndex = 1;
            Assert.True(uut.MoreCommand.CanExecute(uut));
        }

        [Test]
        public void MoreCommand_CanExecuteMinus1_False()
        {
            uut.CurrentIndex = -1;
            Assert.False(uut.MoreCommand.CanExecute(uut));
        }

        [Test]
        public void MoreCommand_Execute_Expect3()
        {
            uut.Add(new PurchasedProduct(new Product(), 2));
            uut.CurrentIndex = 0;
            uut.MoreCommand.Execute(uut);

            Assert.That(uut[uut.CurrentIndex].Quantity, Is.EqualTo(3));
        }

        //Less -----------------------------------------------------------------------
        
        [Test]
        public void LessCommand_CanExecute1_True()
        {
            uut.CurrentIndex = 1;
            Assert.True(uut.LessCommand.CanExecute(uut));
        }

        [Test]
        public void LessCommand_CanExecuteMinus1_False()
        {
            uut.CurrentIndex = -1;
            Assert.False(uut.LessCommand.CanExecute(uut));
        }

        [Test]
        public void LessCommand_Execute_Expect1()
        {
            uut.Add(new PurchasedProduct(new Product(), 2));
            uut.CurrentIndex = 0;
            uut.LessCommand.Execute(uut);

            Assert.That(uut[uut.CurrentIndex].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void LessCommand_Execute_ExpectNone()
        {
            uut.Add(new PurchasedProduct(new Product(), 1));
            uut.CurrentIndex = 0;
            uut.LessCommand.Execute(uut);

            Assert.That(uut, Is.Empty);
        }

        //Prew -----------------------------------------------------------------------

        [Test]
        public void PrevCommand_CanExecute1_True()
        {
            uut.CurrentIndex = 1;
            Assert.True(uut.PrevCommand.CanExecute(uut));
        }

        [Test]
        public void PrevCommand_CanExecuteMinus1_False()
        {
            uut.CurrentIndex = -1;
            Assert.False(uut.PrevCommand.CanExecute(uut));
        }

        [Test]
        public void PrevCommand_Execute_Expect2()
        {
            uut.CurrentIndex = 3;
            uut.PrevCommand.Execute(uut);

            Assert.That(uut.CurrentIndex, Is.EqualTo(2));
        }

        //Next -----------------------------------------------------------------------

        [Test]
        public void NextCommand_CanExecute1_True()
        {
            uut.CurrentIndex = 1;

            uut.Add(new PurchasedProduct(new Product(), 1));
            uut.Add(new PurchasedProduct(new Product(), 1));
            uut.Add(new PurchasedProduct(new Product(), 1));

            Assert.True(uut.NextCommand.CanExecute(uut));
        }

        [Test]
        public void NextCommand_CanExecuteMinus1_False()
        {
            uut.CurrentIndex = -1;
            Assert.False(uut.NextCommand.CanExecute(uut));
        }

        [Test]
        public void NextCommand_Execute_Expect4()
        {
            uut.CurrentIndex = 3;
            uut.NextCommand.Execute(uut);

            Assert.That(uut.CurrentIndex, Is.EqualTo(4));
        }

        //Delete -----------------------------------------------------------------------

        [Test]
        public void DeleteCommand_CanExecute1_True()
        {
            uut.CurrentIndex = 1;
            Assert.True(uut.DeleteCommand.CanExecute(uut));
        }

        [Test]
        public void DeleteCommand_CanExecuteMinus1_False()
        {
            uut.CurrentIndex = -1;
            Assert.False(uut.DeleteCommand.CanExecute(uut));
        }

        [Test]
        public void DeleteCommand_Execute_ExpectNone()
        {
            uut.Add(new PurchasedProduct(new Product(), 1));
            uut.CurrentIndex = 0;
            uut.DeleteCommand.Execute(uut);

            Assert.That(uut, Is.Empty);
        }
    }
}
