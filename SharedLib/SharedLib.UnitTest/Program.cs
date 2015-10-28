using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using SharedLib.Protocol.Commands;
using SharedLib.Protocol;
using SharedLib.Protocol.CmdMarshallers;

namespace SharedLib.UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // Opret produkt
            var product = new Product()
            {
                Name = "Banan",
                Price = 10,
                ProductId = 1,
                ProductNumber = "25"
            };

            //Opret PuchasedProduct
            var pproduct = new PurchasedProduct()
            {
                Name = "Æble",
                ProductNumber = "60",
                Quantity = 10,
                UnitPrice = 5

            };

            // Opret Purchase
            var purchase = new Purchase();
            purchase.PurchasedProducts = new List<PurchasedProduct>();

            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            purchase.PurchasedProducts.Add(pproduct);
            
            // Opret Commands
            var cmd = new CreateProductCmd(product);
            var prcmd = new ProductCreatedCmd(product);
            var gcmd = new GetCatalogueCmd();
            var cdcmd = new CatalogueDetailsCmd();
            var rpcmd = new RegisterPurchaseCmd(purchase);
            var dcmd = new DeleteProductCmd(product);

            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product); // 5
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product);
            cdcmd.Products.Add(product); // 10


            // Create specific marshal
            CreateProductMarshal cmarshal = new CreateProductMarshal();
            ProductCreatedMarshal pmarshal = new ProductCreatedMarshal();
            GetCatalogueMarshal gcmarshal = new GetCatalogueMarshal();
            CatalogueDetailsMarshal cdmarshal = new CatalogueDetailsMarshal();
            RegisterPurchaseMarshal rpmarshal = new RegisterPurchaseMarshal();
            DeleteProductMarshal dmarshal = new DeleteProductMarshal();

            // Create protocol instance
            Protocol.Protocol proto = new Protocol.Protocol();

            // Encode from each
            string xml = cmarshal.Encode(cmd);
            string xml2 = proto.Encode(cmd);
            string xml3 = pmarshal.Encode(prcmd);
            string xml4 = gcmarshal.Encode(gcmd);
            string xml5 = cdmarshal.Encode(cdcmd);
            string xml6 = rpmarshal.Encode(rpcmd);
            string xml7 = dmarshal.Encode(dcmd);

            // Decode from each
            var ccmd2 = (CreateProductCmd)cmarshal.Decode(xml);
            var test2 = (CreateProductCmd)proto.Decode(xml2); // Her er man vel nød til at kende hvilken command der kommer til castingen????
            var test3 = (ProductCreatedCmd) pmarshal.Decode(xml3);
            var test4 = (GetCatalogueCmd) gcmarshal.Decode(xml4);
            var test5 = (CatalogueDetailsCmd) cdmarshal.Decode(xml5);
            var test6 = (RegisterPurchaseCmd) rpmarshal.Decode(xml6);
            var test7 = (DeleteProductCmd) dmarshal.Decode(xml7);

            // Write first test of encode and decode from specific marshal
            Console.WriteLine(xml);
            Console.WriteLine("");
            Console.WriteLine(ccmd2.CmdName);
            Console.WriteLine(ccmd2.Name);
            Console.WriteLine(ccmd2.ProductNumber);
            Console.WriteLine(ccmd2.Price);
            Console.WriteLine("");

            // Write second test of encode and decode from protocol
            Console.WriteLine(xml2);
            Console.WriteLine("");
            Console.WriteLine(test2.Name);
            Console.WriteLine(test2.ProductNumber);
            Console.WriteLine(test2.Price);
            Console.WriteLine("");

            // Write 3rd test of encode and decode from ProductCreateCmd
            Console.WriteLine(xml3);
            Console.WriteLine("");
            Console.WriteLine(test3.Name);
            Console.WriteLine(test3.ProductNumber);
            Console.WriteLine(test3.Price);
            Console.WriteLine(test3.ProductId);
            Console.WriteLine("");

            // 4th test GetCatalogueCmd
            Console.WriteLine(xml4);
            Console.WriteLine("");
            Console.WriteLine(test4.CmdName);
            Console.WriteLine("");

            // 5th test CatalogueDetailsCmd
            Console.WriteLine(xml5);
            Console.WriteLine("");
            Console.WriteLine(test5.Products.ElementAt(0).Name);
            Console.WriteLine(test5.Products.ElementAt(1).Name);
            Console.WriteLine(test5.Products.ElementAt(2).Name);
            Console.WriteLine(test5.Products.ElementAt(3).Name);
            Console.WriteLine(test5.Products.ElementAt(9).Name);
            Console.WriteLine("");

            // 6th test RegisterPurchaseCmd
            Console.WriteLine(xml6);
            Console.WriteLine("");
            Console.WriteLine(test6.Products.ElementAt(0).Name);
            Console.WriteLine(test6.Products.ElementAt(1).Name);
            Console.WriteLine(test6.Products.ElementAt(2).Name);
            Console.WriteLine(test6.Products.ElementAt(3).Name);
            
            // 7th test DeleteProductCmd
            Console.WriteLine(xml7);
            Console.WriteLine("");
            Console.WriteLine(test7.CmdName);
            Console.WriteLine("");
        */
        }
    }
}
