using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedLib.Models;

namespace KasseApparat
{
    public class ProductCategoryList : ObservableCollection<ProductCategory>
    {
        public IDBcontrol _db = new FakeDBcontrol(); //Fake for testing
        //public IDBcontrol _db = new DBcontrol(new Connection("127.0.0.1", 11000));
        public ProductList pl = new ProductList();

        public ProductCategoryList()
        {
            Update();

            foreach (var prod in this[0].Products)
                pl.Add(prod);
        }

        public void Update()
        {
            ClearItems();
            //List<ProductCategory> pc = _db.GetProducts();
            //foreach (var prod in pc) Add(prod);
        }
    }
}
