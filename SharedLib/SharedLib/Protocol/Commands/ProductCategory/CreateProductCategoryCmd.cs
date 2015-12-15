using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    /// <summary>
    /// Request that a productcategory can be created in the database.
    /// </summary>
    public class CreateProductCategoryCmd: Command
    {
        private readonly string _name;

        /// <summary>
        /// List of Product objects in the ProductCategory.
        /// </summary>
        public readonly List<Product> Products = new List<Product>(); 

        /// <summary>
        /// Name of the ProductCategory
        /// </summary>
        public string Name { get { return _name; }}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productCategory">ProductCategory which attributes is to be copied into the command.</param>
        public CreateProductCategoryCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            foreach (var prd in productCategory.Products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the ProductCategory</param>
        /// <param name="products">List of Product objects which is to be copied into a list in the ProductCategory.</param>
        public CreateProductCategoryCmd(string name, List<Product> products)
        {
            _name = name;
            foreach (var prd in products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }
    
    }
}
