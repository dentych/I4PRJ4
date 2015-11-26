using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedLib.Models;

namespace KasseApparat
{
    public interface IPrinter
    {
        void PrintPurchase(List<PurchasedProduct> shoplist);
    }
}
