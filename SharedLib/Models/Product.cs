using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal Price { get; set; }

        public Product()
        {
            
        }

        public Product(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            Price = product.Price;
        }

    }
}