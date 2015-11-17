using System.Collections.Generic;
using SharedLib.Models;

namespace KasseApparat
{
    public interface IDBcontrol
    {
        List<ProductCategory> GetProducts();
        void PurchaseDone(IList<PurchasedProduct> ShopList);
    }
}