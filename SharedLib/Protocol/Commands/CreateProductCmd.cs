using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    public class CreateProductCmd : Command
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly decimal _price;

        public string Name { get { return _name; } }
        public string ProductNumber { get { return _productNumber; } }
        public decimal Price { get { return _price; } }

        public CreateProductCmd(string name, string productNumber, decimal price)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
        }

        public CreateProductCmd(Product product)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
        }
    }
}
