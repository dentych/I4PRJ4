using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using SharedLib.Models;
using MvvmFoundation.Wpf;
using System.Linq;
using System.Windows.Input;

namespace KasseApparat
{
    /*Denne klasse symboliserer en knap i grænsefladen under inputs. Derved kan hver
    knaps individuelle funktionalitet skrives 1 sted og så bruges flere gange.*/
    public class ButtonContent : INotifyPropertyChanged
    {
        private string _name;
        private string _price;
        readonly ShoppingList _shopList;

        /*
         * Ctor: Opretter en knap udfra et givent product.
         */
        public ButtonContent(Product p)
        {
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            //Check if recieved product contains a product
            if (!string.IsNullOrEmpty(p.Name))
            {
                Name = p.Name;
                Price = p.Price + " kr.";
            }
            else
            {
                this.Name = string.Empty;
                this.Price = string.Empty;
            }

            Product = p;
        }

        /*
         * Ctor: Opretter en knap udfra et navn og en pris
         */
        public ButtonContent(string name, string price)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.Name = name;
                this.Price = price + " kr.";
            }
            else
            {
                this.Name = string.Empty;
                this.Price = string.Empty;
            }
        }

        /*
         * Property for pris, bruges i grænsefladen til at sætte knappens pris
         */
        public string Price
        {
            set
            {
                _price = value;
                Notify(string.Empty);
            }
            get
            {
                return _price;
            }
        }

        /*
         * Property for navn, bruges i grænsefladen til at sætte knappens navn
         */
        public string Name
        {
            set
            {
                _name = value;
                Notify(string.Empty);
            }
            get
            {
                return _name;
            }
        }

        /*
         * Giver adgang til det produkt som knappen symboliserer. Derved kan man nemt
         * få adgang til det produkt som skal oprettes i indkøbskurven ved tryk på
         * knappen.
         */
        public Product Product;

        ICommand _AddCommand;
        public ICommand AddCommand { get { return _AddCommand ?? (_AddCommand = new RelayCommand(AddCommandExecute, AddCommandCanExecute)); } }

        /*
         * AddCommand: En command som kaldes ved tryk på en knap i grænsefladen. Denne tilføjer så et nyt
         * produkt til shoppinglist hvis produktet ikke eksisterer, eller incrementerer mængden af produktet
         * hvis det allerede eksisterer i listen.
         */
        void AddCommandExecute()
        {

            //This functionality must be moved to shoppinglist. Belong Here It Does Not
            if (_shopList.Any(x => x.Name == Name))
            {
                //Retrieve index of existing item on list.
                int index = _shopList.IndexOf(_shopList.Single(x => x.Name == Name));
                //Increment item in shoppinglist
                _shopList.IncrementQuantity(index);
            }
            else
            {
                //Adds new item on shoppinglist.
                _shopList.AddItem(new PurchasedProduct(Product, 1, 1));
            }
        }

        /*
         * Denne bestemmer om knappen er enabled. Hvis der altså ikke er indhold i strengen
         * der symboliserer dens navn eller dens pris.
         */
        bool AddCommandCanExecute()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price))
                return false;
            else
                return true;
        }

        /* 
         * Property changed event handler: Giver grænsefladen besked hvis der er en ændring i en
         * given property. Derved ved view at den skal opdatere sig selv og afspejle det indhold
         * der ligger i model laget
         */
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}