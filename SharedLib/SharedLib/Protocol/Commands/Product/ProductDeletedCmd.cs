using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// Information that a product has been deleted.
    /// </summary>
    public class ProductDeletedCmd: Command
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
        /// <param name="productId">ProductId of the product that has been deleted.</param>
        public ProductDeletedCmd(int productId)
        {
            _productId = productId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">Product which attributes is to be copied into the command.</param>
        public ProductDeletedCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productId = product.ProductId;
            _productCategoryId = product.ProductCategoryId;
        }
    }
}
