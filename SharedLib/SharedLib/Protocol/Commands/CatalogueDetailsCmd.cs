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
        public readonly List<ProductCategory> ProductCategories = new List<ProductCategory>();

        public CatalogueDetailsCmd()
        {
        }

        public CatalogueDetailsCmd(List<ProductCategory> productCategories )
        {
            foreach (var prdC in productCategories)
            {
                var prdCCopy = new ProductCategory()
                {
                    Name = prdC.Name,
                    ProductCategoryId = prdC.ProductCategoryId
                };

                foreach (var product in prdC.Products)
                    prdCCopy.Products.Add(product);

                ProductCategories.Add(prdCCopy);
            }
        }

        public Catalogue GetCatalogue()
        {
            var catalogue = new Catalogue();
            catalogue.ProductCategories.AddRange(ProductCategories);
            return catalogue;
        }
    }
}
