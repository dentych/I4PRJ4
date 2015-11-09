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

        public string Name { get { return _name; } }
        public string ProductNumber { get { return _productNumber; } }
        public decimal Price { get { return _price; } }
        public int ProductId { get { return _productId; } }

        public ProductCreatedCmd(string name, string productNumber, decimal price, int productId)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
            _productId = productId;
        }

        public ProductCreatedCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productId = product.ProductId;
        }

        public Product GetProduct()
        {
            return new Product()
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                ProductNumber = ProductNumber,
            };
        }
    }
}
