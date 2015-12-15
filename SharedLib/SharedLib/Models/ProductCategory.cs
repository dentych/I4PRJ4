using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{   
    /// <summary>
    /// Datamodel for the productcategories
    /// </summary>
    public class ProductCategory
    {
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// List which holds all the products within the category
        /// </summary>
        public IList<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductCategory() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="products"> List of products to copy into the productcategory</param>
        public ProductCategory(IList<Product> products)
        {
            foreach (var prd in products)
                Products.Add(prd);
        }
    }
}
