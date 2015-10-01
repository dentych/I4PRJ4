﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace KasseApparat
{
    interface IDBcontrol
    {
        List<Product> GetProducts();
    }

    class FakeDBcontrol : IDBcontrol
    {
        public List<Product> GetProducts()
        {
            Product p1 = new Product();
            p1.Name = "Beer";
            p1.Price = 12;
            p1.ProductId = 0;
            p1.ProductNumber = "0";

            Product p2 = new Product();
            p2.Name = "Vodka";
            p2.Price = 40;
            p2.ProductId = 1;
            p2.ProductNumber = "1";

            Product p3 = new Product();
            p3.Name = "Whiskey";
            p3.Price = 100;
            p3.ProductId = 2;
            p3.ProductNumber = "2";

            List<Product> PL = new List<Product>();

            PL.Add(p1);
            PL.Add(p2);
            PL.Add(p3);

            return PL;
        }
    }

    class DBcontrol : IDBcontrol
    {
        public List<Product> GetProducts()
        {

            List<Product> PL = new List<Product>();
            return PL;
        }
    }


}
