using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SharedLib.Models;

namespace KasseApparat
{
    public class ButtonContent : INotifyPropertyChanged
    {
        private string _name;
        private string _price;

        public ButtonContent(Product p)
        {
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

        public bool ButtonIsEnabled
        {
            get { return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price)); }
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