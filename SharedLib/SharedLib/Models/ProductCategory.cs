using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class ProductCategory
    {
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();

        public ProductCategory() { }

        public ProductCategory(IList<Product> products)
        {
            foreach (var prd in products)
                Products.Add(prd);
        }
    }
}
