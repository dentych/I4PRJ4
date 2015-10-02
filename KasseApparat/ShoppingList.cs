using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace KasseApparat
{
    class ShoppingList : ObservableCollection<PurchasedProduct>
    {
        public ShoppingList()
        {
            Product p1 = new Product();
            p1.Name = "Beer";
            p1.Price = 12;
            p1.ProductId = 0;
            p1.ProductNumber = "0";

            Product p2 = new Product();
            p2.Name = "Chips";
            p2.Price = 20;
            p2.ProductId = 1;
            p2.ProductNumber = "1";

            Add(new PurchasedProduct(p1, 6));
            Add(new PurchasedProduct(p2, 1));
        }

        public int TotalPrice()
        {
            return this.Sum(vare => (int) vare.TotalPrice);
        }

#region Index
        private int _currentIndex;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                Notify();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
#endregion
    }
}
