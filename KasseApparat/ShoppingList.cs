using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public int TotalWarePrice()
        {
            int tot = 0;
            foreach (var Vare in this)
            {
                tot += (int)Vare.TotalPrice;
            }
            return tot;
        }
    }
}
