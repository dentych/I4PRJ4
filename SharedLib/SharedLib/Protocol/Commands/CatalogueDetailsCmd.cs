using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// A command which contains the productcatelogue, which is a list of productcategories.
    /// </summary>
    public class CatalogueDetailsCmd: Command
    {
        public readonly List<ProductCategory> ProductCategories = new List<ProductCategory>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public CatalogueDetailsCmd()
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productCategories">List of ProductCategory objects each with a list of Product objects which is to be copied.</param>
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

        /// <summary>
        /// Returns an instance of the productcatalogue
        /// </summary>
        /// <returns>A catalogue datamodel</returns>
        public Catalogue GetCatalogue()
        {
            var catalogue = new Catalogue();
            catalogue.ProductCategories.AddRange(ProductCategories);
            return catalogue;
        }
    }
}
