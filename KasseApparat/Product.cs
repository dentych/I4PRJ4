using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    class Product
    {
        private string _name;
        private int _price;
        private string _id;
        private int _amount;

        public Product(string name, int price, string id, int amount = 1)
        {
            _name = name;
            _price = price;
            _id = id;
            _amount = amount;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Ammount
        {
            get { return _ammount; }
            set { _ammount = value; }
        }
    }
}
