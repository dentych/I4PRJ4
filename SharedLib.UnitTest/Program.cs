using System;
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
            // Opret CreateProductCmd
            var cmd = new CreateProductCmd(product);
            
            // Create specific marshal
            CreateProductMarshal cmarshal = new CreateProductMarshal();
            // Create protocol instance
            Protocol.Protocol proto = new Protocol.Protocol();

            // Encode from each
            string xml = cmarshal.Encode(cmd);
            string xml2 = proto.Encode(cmd);

            // Decode from each
            var ccmd2 = (CreateProductCmd)cmarshal.Decode(xml);
            var test2 = (CreateProductCmd)proto.Decode(xml2); // Her er man vel nød til at kende hvilken command der kommer til castingen????
            
            // Write first test of encode and decode from specific marshal
            Console.WriteLine(xml);
            Console.WriteLine("");
            Console.WriteLine(ccmd2.Name);
            Console.WriteLine(ccmd2.ProductNumber);
            Console.WriteLine(ccmd2.Price);
            Console.WriteLine("");

            // Write second test of encode and decode from protocol
            Console.WriteLine(xml2);
            Console.WriteLine("");
            Console.WriteLine(test2.Name);
            Console.WriteLine(test2.ProductNumber);
            Console.WriteLine(test2.Price);*/
        }
    }
}
