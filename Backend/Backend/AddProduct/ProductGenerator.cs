using System;
using System.Collections.Generic;
using SharedLib.Models;

namespace Backend.AddProduct
{
    public interface IProductGenerator
    {
        Product GenerateProduct(Dictionary<string,string> data);
    }

    public class ProductGenerator : IProductGenerator
    {
        public Product GenerateProduct(Dictionary<string,string> data)
        {
            var product = new Product();
            product.Name = data["NAME"];
            product.ProductNumber = data["BARCODE"];
            product.Price = Decimal.Parse(data["PRICE"]);

            return product;
        }

    }
}