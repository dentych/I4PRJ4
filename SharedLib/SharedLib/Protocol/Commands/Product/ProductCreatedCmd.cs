using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    public class ProductCreatedCmd: Command
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly decimal _price;
        private readonly int _productId;
        private readonly int _productCategoryId;

        public string Name { get { return _name; } }
        public string ProductNumber { get { return _productNumber; } }
        public decimal Price { get { return _price; } }
        public int ProductId { get { return _productId; } }
        public int ProductCategoryId { get { return _productCategoryId; } }

        public ProductCreatedCmd(string name, string productNumber, decimal price, int productId, int productCategoryId)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
            _productId = productId;
            _productCategoryId = productCategoryId;
        }

        public ProductCreatedCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productId = product.ProductId;
            _productCategoryId = product.ProductCategoryId;
        }

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
