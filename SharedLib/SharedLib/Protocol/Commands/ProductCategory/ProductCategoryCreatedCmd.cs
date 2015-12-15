using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    /// <summary>
    /// Information that a new ProductCategory has been created.
    /// </summary>
    public class ProductCategoryCreatedCmd: Command
    {
        private readonly string _name;
        private readonly int _productCategoryId;

        /// <summary>
        /// List of Product objects in the ProductCategory.
        /// </summary>
        public readonly List<Product> Products = new List<Product>();

        /// <summary>
        /// Name of the ProductCategory
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// ProductCategoryId of the ProductCategory.
        /// </summary>
        public int ProductCategoryId { get { return _productCategoryId; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productCategory">ProductCategory which attributes is to be copied into the command.</param>
        public ProductCategoryCreatedCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            _productCategoryId = productCategory.ProductCategoryId;
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
        /// <param name="productCategoryId">ProductCategoryId of the ProductCategory</param>
        /// <param name="products">List of Product objects which is to be copied into a list in the ProductCategory.</param>
        public ProductCategoryCreatedCmd(string name, int productCategoryId, List<Product> products)
        {
            _name = name;
            _productCategoryId = productCategoryId;
            foreach (var prd in products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }
    }
}
