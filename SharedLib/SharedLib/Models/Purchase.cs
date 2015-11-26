using System;
using System.Collections.Generic;

namespace SharedLib.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<PurchasedProduct> PurchasedProducts { get; set; } = new List<PurchasedProduct>();

        public Purchase()
        {
            DateCreated = DateTime.Now;
        }
    }
}
