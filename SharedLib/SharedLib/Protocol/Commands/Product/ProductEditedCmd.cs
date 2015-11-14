﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    public class ProductEditedCmd: Command
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly decimal _price;
        private readonly int _productId;
        private readonly int _productCategoryId;
        private readonly int _oldProductCategoryId;

        public string Name { get { return _name; } }
        public string ProductNumber { get { return _productNumber; } }
        public decimal Price { get { return _price; } }
        public int ProductId { get { return _productId; } }
        public int ProductCategoryId { get { return _productCategoryId; } }
        public int OldProductCategoryId { get { return _oldProductCategoryId; } }

        public ProductEditedCmd(string name, string productNumber, decimal price, int productId, int productCategoryId, int oldProductCategoryId)
        {
            _name = name;
            _productNumber = productNumber;
            _price = price;
            _productId = productId;
            _productCategoryId = productCategoryId;
            _oldProductCategoryId = oldProductCategoryId;
        }

        public ProductEditedCmd(Product product, int oldProductCategoryId)
        {
            _name = product.Name;
            _productNumber = product.ProductNumber;
            _price = product.Price;
            _productId = product.ProductId;
            _productCategoryId = product.ProductCategoryId;
            _oldProductCategoryId = oldProductCategoryId;
        }
    }
}
