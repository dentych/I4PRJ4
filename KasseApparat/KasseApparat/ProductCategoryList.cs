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

        /// <summary>
        /// Contructor, kalder Update() for at Clienten starter op med produkter på listen
        /// </summary>
        public ProductCategoryList()
        {
            Update();
        }

        /// <summary>
        /// Update funktion som henter produkt oversigt fra central server og sætter disse som de tilgængelige produkter
        /// </summary>
        public void Update()
        {
            ClearItems();
            List<ProductCategory> pc = _db.GetProducts();
            foreach (var prod in pc) Add(prod);
        }
    }
}
