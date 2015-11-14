using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Datamodels;
using SharedLib.Models;

namespace Backend.Fakegenerator
{
    internal class FakeMaker
    {
        public BackendProductCategoryList Make()
        {
            var categories = new BackendProductCategoryList();
            /* Opret fake produkter */
            for (var i = 0; i < 10; i++)
            {
                var newCategory = new BackendProductCategory
                {
                    BName = "Category " + (i + 1),
                    ProductCategoryId = i+1,
                    Products = new List<Product>()
                };
                for (var x = 0; x < 10; x++)
                {
                    var tmpProduct = new BackendProduct
                    {
                        BName = "Name " + i,
                        BPrice = (7*x)/2,
                        BProductNumber = "ABC" + i
                    };
                    newCategory.Products.Add(tmpProduct);
                }
                categories.Add(newCategory);
            }
            return categories;
        }
    }
}