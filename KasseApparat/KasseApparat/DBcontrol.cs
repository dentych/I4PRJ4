using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using SharedLib.Protocol;
using SharedLib.Protocol.Commands;

namespace KasseApparat
{
    public interface IDBcontrol
    {
        List<ProductCategory> GetProducts();
        void PurchaseDone(IList<PurchasedProduct> ShopList);
    }

    public class FakeDBcontrol : IDBcontrol
    {
        public List<ProductCategory> GetProducts()
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

            Product p16 = new Product();
            p16.Name = "Malange";
            p16.Price = 52;
            p16.ProductId = 15;
            p16.ProductNumber = "15";

            Product p17 = new Product();
            p17.Name = "Pana Cotta";
            p17.Price = 20;
            p17.ProductId = 16;
            p17.ProductNumber = "16";

            Product p18 = new Product();
            p18.Name = "Saffron";
            p18.Price = 79;
            p18.ProductId = 17;
            p18.ProductNumber = "17";

            Product p19 = new Product();
            p19.Name = "Mælk";
            p19.Price = 4;
            p19.ProductId = 18;
            p19.ProductNumber = "18";

            Product p20 = new Product();
            p20.Name = "Durum rulle";
            p20.Price = 39;
            p20.ProductId = 19;
            p20.ProductNumber = "19";

            Product p21 = new Product();
            p21.Name = "Aged Whisky";
            p21.Price = 299;
            p21.ProductId = 20;
            p21.ProductNumber = "20";

            Product p22 = new Product();
            p22.Name = "Lasagne";
            p22.Price = 34;
            p22.ProductId = 21;
            p22.ProductNumber = "21";

            Product p23 = new Product();
            p23.Name = "Sprite";
            p23.Price = 12;
            p23.ProductId = 22;
            p23.ProductNumber = "22";

            Product p24 = new Product();
            p24.Name = "Peas";
            p24.Price = 24;
            p24.ProductId = 23;
            p24.ProductNumber = "23";

            Product p25 = new Product();
            p25.Name = "Kaffe";
            p25.Price = 55;
            p25.ProductId = 24;
            p25.ProductNumber = "24";
            
      
            List<Product> PL1 = new List<Product>();
            List<Product> PL2 = new List<Product>();
            PL1.Add(p1);
            PL1.Add(p2);
            PL1.Add(p3);
            PL1.Add(p4);
            PL2.Add(p5);
            PL2.Add(p6);
            PL2.Add(p7);
            PL2.Add(p8);
            PL2.Add(p9);
            PL2.Add(p10);
            PL2.Add(p11);
            PL2.Add(p12);
            PL2.Add(p13);
            PL2.Add(p14);
            PL2.Add(p15);
            PL2.Add(p16);
            PL2.Add(p17);
            PL2.Add(p18);
            PL2.Add(p19);
            PL2.Add(p20);
            PL2.Add(p21);
            PL2.Add(p22);
            PL2.Add(p23);
            PL2.Add(p24);
            PL2.Add(p25);

            List<ProductCategory> PC = new List<ProductCategory>();
            ProductCategory PC1 = new ProductCategory();
            PC1.Name = "Booze";
            PC1.Products = PL1;
            ProductCategory PC2 = new ProductCategory();
            PC2.Name = "Other";
            PC2.Products = PL2;
            PC.Add(PC1);
            PC.Add(PC2);
            return PC;
        }

        public void PurchaseDone(IList<PurchasedProduct> ShopList)
        {}
    }

    public class DBcontrol : IDBcontrol
    {
        public IConnection Connection = null;
        public IProtocol protocol = new Protocol();

        public DBcontrol(IConnection conn)
        {
            Connection = conn;
        }

        public List<ProductCategory> GetProducts()
        {
            Connection.Connect();

            Connection.Send(protocol.Encode(new GetCatalogueCmd()));
            var cmd = (CatalogueDetailsCmd)protocol.Decode(Connection.Receive());

            Connection.Disconnect();

            return cmd.ProductCategories;
        }

        public void PurchaseDone(IList<PurchasedProduct> ShopList)
        {
            Connection.Connect();

            Purchase pc = new Purchase();
            pc.PurchasedProducts = (List<PurchasedProduct>)ShopList;

            Connection.Send(protocol.Encode(new RegisterPurchaseCmd(pc)));

            Connection.Disconnect();
        }
    }


}
