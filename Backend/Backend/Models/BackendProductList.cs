using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BackendProductList : ObservableCollection<Product>
    {
        #region Properties
        private int _curIndex = -1;

        public int CurrentIndex
        {
            get { return _curIndex; }
            set { _curIndex = value; }
        }
        #endregion

        public BackendProductList()
        {
            Product product;

            for (int i = 0; i < 10; i++)
            {
                product = new Product();
                product.Name = "Product #" + i.ToString();
                product.Price = i * 2;
                product.ProductNumber = "PRODUCT" + i.ToString();

                Add(product);
            }
        }
    }
}
