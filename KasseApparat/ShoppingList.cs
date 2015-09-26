using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    class ShoppingList : ObservableCollection<Product>
    {
        private int _totalPrice;

        public ShoppingList()
        {
            Add(new Product("Beer", 12, "00", 6));
            Add(new Product("Chips", 20, "01"));
        }

        public void add(Product product)
        {
            Add(product);
        }

        public void Remove(string name)
        {
            foreach (var product in this)
            {
                if (product.Name == name)
                {
                    Remove(product);
                }
            }
        }

        public int TotalWarePrice
        {
            get { return this.Sum(Vare => (Vare.Price * Vare.Amount)); }
            private set { _totalPrice = value; }
            //return this.Sum(Vare => (Vare.Price*Vare.Amount));
        }

        public int TotalPrice()
        {
            return this.Sum(Vare => (Vare.Price*Vare.Amount));
        }
    }
}
