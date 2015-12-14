using System;
using System.Collections.Generic;

namespace SharedLib.Models
{
    /// <summary>
    /// Datamodel for a purchase, primarily used to create receipts
    /// </summary>
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// List of PurchasedProducts to make up the purchase.
        /// </summary>
        public virtual List<PurchasedProduct> PurchasedProducts { get; set; } = new List<PurchasedProduct>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Purchase()
        {
            DateCreated = DateTime.Now;
        }
    }
}
