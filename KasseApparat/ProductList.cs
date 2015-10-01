using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace KasseApparat
{
    class ProductList : ObservableCollection<Product>
    {
        private readonly IDBcontrol _db = new FakeDBcontrol(); //Fake for testing

        public ProductList() { }

        public void Update()
        {
            List<Product> pl = _db.GetProducts();
            foreach (var prod in pl) Add(prod);
        }
    }
}
