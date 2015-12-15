using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands
{
    /// <summary>
    /// Requests that a purchase gets saved in the database
    /// </summary>
    public class RegisterPurchaseCmd: Command
    {
        /// <summary>
        /// List of PurchasedProduct objects to be saved.
        /// </summary>
        public readonly List<PurchasedProduct> Products 
            = new List<PurchasedProduct>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="purchase">Purchase datamodel to be copied into the command.</param>
        public RegisterPurchaseCmd(Purchase purchase)
        {
            foreach (var prd in purchase.PurchasedProducts)
            {
                var copy = new PurchasedProduct(prd);
                Products.Add(copy);
            }
        }
    
    }
}
