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
        public ShoppingList() { }

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

        public void ChangeAmmount(string name, int newAmt)
        {
            foreach (var product in this)
            {
                if (product.Name == name)
                {
                    product.Ammount = newAmt;
                }
            } 
        }
    }
}
