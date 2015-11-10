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
    public class ProductList : ObservableCollection<Product>
    {
        private IDBcontrol _db = new FakeDBcontrol(); //Fake for testing
        //private IDBcontrol _db = new DBcontrol(new Connection("127.0.0.1", 11000));
        public IDBcontrol Db { set { _db = value; }}

        public ProductList()
        {
            Update();
        }

        public void Update()
        {
            //ClearItems();
            //List<Product> pl = _db.GetProducts();
            //foreach (var prod in pl) Add(prod);
        }
    }
}
