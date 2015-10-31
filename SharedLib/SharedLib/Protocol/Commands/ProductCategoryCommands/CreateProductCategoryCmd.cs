using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    public class CreateProductCategoryCmd: Command
    {
        private readonly string _name;
        public readonly List<Product> Products = new List<Product>(); 

        public string Name { get { return _name; }}

        public CreateProductCategoryCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            foreach (var prd in productCategory.Products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            }
        }

        public CreateProductCategoryCmd(string name, List<Product> products)
        {
            _name = name;
            foreach (var prd in products)
            {
                var copy = new Product(prd);
                Products.Add(copy);
            };
        }
    
    }
}
