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

        public ButtonContent(string name, string price)
        {
            if (name != null)
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
            get { return false; }
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
                Notify(string.Empty);
                return _price;
            }
            
        }

        public string Name;

        public new event PropertyChangedEventHandler PropertyChanged;

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