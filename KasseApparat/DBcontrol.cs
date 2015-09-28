using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    interface IDBcontrol
    {
        void GetProducts(ProductList sl);
    }

    class FakeDBcontrol : IDBcontrol
    {
        public void GetProducts(ProductList pl)
        {
            //pl.Add(new Product("Beer", 12, "00", 6));
            //pl.Add(new Product("Chips", 20, "01"));
        }
    }

    class DBcontrol : IDBcontrol
    {
        public void GetProducts(ProductList pl)
        {
        }
    }


}
