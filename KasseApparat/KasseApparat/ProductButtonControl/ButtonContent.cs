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
    /// <summary>
    ///     Denne klasse symboliserer en knap i grænsefladen under inputs. Derved kan hver  
    ///     knaps individuelle funktionalitet skrives 1 sted og så bruges flere gange. I henhold til MVVM
    ///     er denne klasse viewmodel for knapperne.
    /// </summary>
    public class ButtonContent : INotifyPropertyChanged, IButtonContent
    {
        private string _name;
        private string _price;
        readonly ShoppingList _shopList;

        /// <summary>
        ///     Ctor: Opretter en knap udfra indholdet af et produkt kaldet buttonProduct. Opretter variabler og opretter også en forbindelse
        ///     til ShoppingList objektet fra viewet, derved kan produktet tilknyttet knappen tilføjes til shoppinglist ved et tryk.
        /// </summary>
        /// <param name="buttonProduct"></param>
        /// <param name="sl"></param>
        public ButtonContent(Product buttonProduct, ShoppingList sl = null)
        {
            if (sl == null)
            {
                _shopList = (ShoppingList) Application.Current.MainWindow.FindResource("ShoppingList");
            }
            else
            {
                _shopList = sl;
            }

            //Check if recieved product contains a product
            if (!string.IsNullOrEmpty(buttonProduct.Name))
            {
                Name = buttonProduct.Name;
                Price = buttonProduct.Price + " kr.";
            }
            else
            {
                this.Name = string.Empty;
                this.Price = string.Empty;
            }

            Product = buttonProduct;
        }

        /// <summary>
        ///     Ctor: Opretter en knap udfra indholdet af 2 strenge, en med navn og en med pris. Denne contructor bruges primært til at skabe tomme knapper
        ///     når der ikke er flere produkter at tilføjes.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
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

        /// <summary>
        ///     En property der returnerer prisen på den vare knappen symboliserer. Bruges til at vise prisen på den pågældende knap.
        /// </summary>
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

        /// <summary>
        ///     Giver adgang til det produkt som knappen symboliserer. Derved kan man nemt
        ///     få adgang til det produkt som skal oprettes i indkøbskurven ved tryk på knappen.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        ///     En property der returnerer navnet på den vare knappen symboliserer. Bruges til at vise prisen på den pågældende knap.
        /// </summary>
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

        ICommand _AddCommand;
        public ICommand AddCommand { get { return _AddCommand ?? (_AddCommand = new RelayCommand(AddCommandExecute, AddCommandCanExecute)); } }

        /// <summary>
        ///     AddCommand: En command som kaldes ved tryk på en knap i grænsefladen. Denne tilføjer så et nyt
        ///     produkt til shoppinglist hvis produktet ikke eksisterer, eller incrementerer mængden af produktet
        ///     hvis det allerede eksisterer i listen.
        /// </summary>
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

        /// <summary>
        ///     Dette command bestemmer om knappen er enabled. Hvis der ikke er indhold i knappen kan knappen ikke trykkes på.
        /// </summary>
        /// <returns></returns>
        bool AddCommandCanExecute()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price))
                return false;
            else
                return true;
        }

        /// <summary>
        ///     Property changed event handler: Giver grænsefladen besked hvis der er en ændring i en given property.
        ///     Derved ved view at den skal opdatere sig selv og afspejle det indhold der ligger i model laget.
        /// </summary>
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