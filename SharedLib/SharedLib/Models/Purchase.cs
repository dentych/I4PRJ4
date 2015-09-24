using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public readonly List<PurchasedProduct> Products = new List<PurchasedProduct>();
    }
}
