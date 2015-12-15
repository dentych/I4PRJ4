using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    /// <summary>
    /// Datamodel for the products
    /// </summary>
    public class Product
    {   
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Product() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">product which attributes is to be copied</param>
        public Product(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            Price = product.Price;
            ProductCategoryId = product.ProductCategoryId;
        }
    }
}