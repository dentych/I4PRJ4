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

        public int TotalPrice()
        {
            int tot = 0;
            foreach (var Vare in this)
            {
                tot += (Vare.Price * Vare.Amount);
            }
            return tot;
        }

    }
}
