using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public virtual List<PurchasedProduct> PurchasedProducts { get; set; } = new List<PurchasedProduct>();
    }
}
