using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    public class RegisterPurchaseCmd: Command
    {
        public readonly List<PurchasedProduct> Products 
            = new List<PurchasedProduct>();

        public RegisterPurchaseCmd(Purchase purchase)
        {
            foreach (var prd in purchase.Products)
            {
                var copy = new PurchasedProduct(prd);
                Products.Add(copy);
            }
        }
    
    }
}
