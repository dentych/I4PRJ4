using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    public class ProductCategoryEditedCmd: Command
    {
        private readonly string _name;
        private readonly int _productCategoryId;
        public readonly List<Product> Products = new List<Product>(); 

        public string Name { get { return _name; }}
        public int ProductCategoryId { get { return _productCategoryId; } }

        public ProductCategoryEditedCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            _productCategoryId = productCategory.ProductCategoryId;
            foreach (var prd in productCategory.Products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }

        public ProductCategoryEditedCmd(string name, int productCategoryId, List<Product> products)
        {
            _name = name;
            _productCategoryId = productCategoryId;
            foreach (var prd in products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }
    }
}
