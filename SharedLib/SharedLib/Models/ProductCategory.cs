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
        public List<Product> Products;

        public ProductCategory()
        {
        }

        public ProductCategory(List<Product> products)
        {
            Products = products;
        }
    }
}
