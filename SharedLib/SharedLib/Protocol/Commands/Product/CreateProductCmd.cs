using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// Requests that a product gets created in the database.
    /// </summary>
    public class CreateProductCmd : Command
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly decimal _price;
        private readonly int _productCategoryId;

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get { return _name; }}

        /// <summary>
        /// Product Number of the product.
        /// </summary>
        public string ProductNumber { get { return _productNumber; } }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get { return _price; } }

        /// <summary>
        /// ProductCategoryId of the product.
        /// </summary>
        public int ProductCategoryId { get { return _productCategoryId; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the product to be created</param>
        /// <param name="productNumber">Productnumber of the product</param>
        /// <param name="price">Price of the product</param>
        /// <param name="categoryId">ProductCategoryId of the product</param>
        public CreateProductCmd(string name, string productNumber, decimal price, int categoryId)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
            _productCategoryId = categoryId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">product which attributes is to be copied into the command</param>
        public CreateProductCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productCategoryId = product.ProductCategoryId;
        }
    }
}
