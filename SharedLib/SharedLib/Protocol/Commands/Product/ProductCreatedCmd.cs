using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// Information that a new product has been created.
    /// </summary>
    public class ProductCreatedCmd: Command
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly decimal _price;
        private readonly int _productId;
        private readonly int _productCategoryId;

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Product Number of the product.
        /// </summary>
        public string ProductNumber { get { return _productNumber; } }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get { return _price; } }

        /// <summary>
        /// ProductId of the product
        /// </summary>
        public int ProductId { get { return _productId; } }

        /// <summary>
        /// ProductCategoryId of the product.
        /// </summary>
        public int ProductCategoryId { get { return _productCategoryId; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the product that has been created</param>
        /// <param name="productNumber">Productnumber of the product</param>
        /// <param name="price">Price of the product</param
        /// <param name="productId">ProductId of the product</param>
        /// <param name="productCategoryId">ProductCategoryId of the product</param>
        public ProductCreatedCmd(string name, string productNumber, decimal price, int productId, int productCategoryId)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
            _productId = productId;
            _productCategoryId = productCategoryId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">Product which attributes is to be copied into the command.</param>
        public ProductCreatedCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productId = product.ProductId;
            _productCategoryId = product.ProductCategoryId;
        }

        /// <summary>
        /// Returns a new instance of a Product from the attributes in the command.
        /// </summary>
        /// <returns>an instance of Product</returns>
        public Product GetProduct()
        {
            return new Product()
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                ProductNumber = ProductNumber,
                ProductCategoryId = ProductCategoryId
            };
        }
    }
}
