using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class PurchasedProduct
    {
        public int PurchasedProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get { return Quantity*UnitPrice; } }

        public PurchasedProduct()
        {
        }

        public PurchasedProduct(Product product, int quantity)
        {
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            UnitPrice = product.Price;
            Quantity = quantity;
        }
    }
}
