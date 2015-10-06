using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace KasseApparat
{
    public interface IDBcontrol
    {
        List<Product> GetProducts();
    }

    public class FakeDBcontrol : IDBcontrol
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

            Product p4 = new Product();
            p4.Name = "Rum";
            p4.Price = 99;
            p4.ProductId = 3;
            p4.ProductNumber = "3";

            Product p5 = new Product();
            p5.Name = "Morgan og Cola";
            p5.Price = 49;
            p5.ProductId = 4;
            p5.ProductNumber = "4";

            Product p6 = new Product();
            p6.Name = "Kage";
            p6.Price = 49;
            p6.ProductId = 5;
            p6.ProductNumber = "5";

            Product p7 = new Product();
            p7.Name = "Brie";
            p7.Price = 49;
            p7.ProductId = 6;
            p7.ProductNumber = "6";

            Product p8 = new Product();
            p8.Name = "Gorgonzola";
            p8.Price = 49;
            p8.ProductId = 7;
            p8.ProductNumber = "7";

            Product p9 = new Product();
            p9.Name = "Ravioli";
            p9.Price = 49;
            p9.ProductId = 8;
            p9.ProductNumber = "8";

            Product p10 = new Product();
            p10.Name = "Pizza";
            p10.Price = 49;
            p10.ProductId = 9;
            p10.ProductNumber = "9";

            Product p11 = new Product();
            p11.Name = "Coca Cola";
            p11.Price = 49;
            p11.ProductId = 10;
            p11.ProductNumber = "10";

            Product p12 = new Product();
            p12.Name = "Mojito";
            p12.Price = 49;
            p12.ProductId = 11;
            p12.ProductNumber = "11";

            Product p13 = new Product();
            p13.Name = "Kartofler";
            p13.Price = 49;
            p13.ProductId = 12;
            p13.ProductNumber = "12";

            Product p14 = new Product();
            p14.Name = "Burger";
            p14.Price = 49;
            p14.ProductId = 13;
            p14.ProductNumber = "13";

            Product p15 = new Product();
            p15.Name = "Bearnaise";
            p15.Price = 49;
            p15.ProductId = 14;
            p15.ProductNumber = "14";

            List<Product> PL = new List<Product>();

            PL.Add(p1);
            PL.Add(p2);
            PL.Add(p3);
            PL.Add(p4);
            PL.Add(p5);
            PL.Add(p6);
            PL.Add(p7);
            PL.Add(p8);
            PL.Add(p9);
            PL.Add(p10);
            PL.Add(p11);
            PL.Add(p12);
            PL.Add(p13);
            PL.Add(p14);
            PL.Add(p15);

            return PL;
        }
    }

    class DBcontrol : IDBcontrol
    {
        public IConnection Connection = null;

        public DBcontrol(IConnection conn)
        {
            Connection = conn;
        }

        public List<Product> GetProducts()
        {
            List<Product> PL = new List<Product>();
            return PL;
        }
    }


}
