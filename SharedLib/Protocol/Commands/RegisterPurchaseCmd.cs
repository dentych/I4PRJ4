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
        private readonly int _purchaseId;
        public readonly List<PurchasedProduct> Products; 
        public int PurchaseId { get{ return _purchaseId;}}

        RegisterPurchaseCmd(int purchaseId, List<PurchasedProduct> products )
        {
            _purchaseId = purchaseId;
            Products = products;
        }

        RegisterPurchaseCmd(Purchase purchase)
        {
            _purchaseId = purchase.PurchaseId;
            Products = purchase.Products;
        }
    
    }
}
