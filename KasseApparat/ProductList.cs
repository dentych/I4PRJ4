﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasseApparat
{
    class ProductList : ObservableCollection<Product>
    {
        public ProductList() { }

        public void Update() { }
    }
}
