using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    public class CatalogueDetailsCmd: Command
    {
        public readonly List<Product> Products = new List<Product>();

        public CatalogueDetailsCmd()
        {
        }

        public CatalogueDetailsCmd(List<Product> products )
        {
            foreach (var prd in products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }
    }

        



}
