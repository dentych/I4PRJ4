using System;
using SharedLib.Models;

namespace Backend.AddProduct
{
    public interface IProductGenerator
    {
        Product GenerateProduct();
    }

    public class ProductGenerator : IProductGenerator
    {
        private AddProductCB _addProductCb;

        public ProductGenerator(AddProductCB addProductCb)
        {
            _addProductCb = addProductCb;
        }

        public Product GenerateProduct()
        {
            var product = new Product();
            product.Name = _addProductCb._window.textboxName.Text;
            product.ProductNumber = _addProductCb._window.textboxBarcode.Text;

            // Convert textboxPrice to decimal number
            try
            {
                product.Price = decimal.Parse(_addProductCb._window.textboxPrice.Text);
            }
            catch (Exception e)
            {
                //LastError = "Error converting price to number";
                return null;
            }

            return product;
        }
    }
}