using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class PurchasedProduct
    {
        public uint Quantity { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get { return Quantity*UnitPrice; } }

        public PurchasedProduct()
        {
        }

        public PurchasedProduct(PurchasedProduct pp)
        {
            Quantity = pp.Quantity;
            Name = pp.Name;
            ProductNumber = pp.ProductNumber;
            UnitPrice = pp.UnitPrice;
        }

        public PurchasedProduct(Product product, uint quantity)
        {
            Name = product.Name;
            ProductNumber = product.ProductNumber;
            UnitPrice = product.Price;
            Quantity = quantity;
        }
    }
}
