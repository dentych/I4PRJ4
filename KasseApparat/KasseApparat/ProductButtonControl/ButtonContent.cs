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
    public class ButtonContent : INotifyPropertyChanged
    {
        private string _name;
        private string _price;
        ShoppingList _shopList;

        public ButtonContent(Product p)
        {
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

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

        public Product Product;

        ICommand _AddCommand;
        public ICommand AddCommand { get { return _AddCommand ?? (_AddCommand = new RelayCommand(AddCommandExecute, AddCommandCanExecute)); } }

        void AddCommandExecute()
        {

            _shopList.AddItem(Product);
        }

        bool AddCommandCanExecute()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price))
                return false;
            else
                return true;
        }

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