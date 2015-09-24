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
        public readonly List<Product> Products;

        CatalogueDetailsCmd(List<Product> products )
        {
            Products = products;
        }

    }
}
