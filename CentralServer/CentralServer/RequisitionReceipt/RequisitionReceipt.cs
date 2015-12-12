using System;
using System.IO;
using System.Text;
using SharedLib.Models;

namespace CentralServer.RequisitionReceipt
{
    public class RequisitionReceipt : IRequisitionReceipt
    {

        public void Write(Purchase purchase)
        {
            var fp = GetFilePath();

            File.AppendAllText(fp, String.Format("{0}\t{1}\r\n", purchase.PurchaseId, purchase.DateCreated.ToString()));

            foreach (var product in purchase.PurchasedProducts)
            {
                var s = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n",
                    product.PurchasedProductId,
                    product.ProductNumber,
                    product.Name,
                    product.Quantity,
                    product.UnitPrice,
                    product.TotalPrice);
                File.AppendAllText(fp, s);
            }

            File.AppendAllText(fp, "\r\n");
        }

        private string GetFilePath()
        {
            var today = DateTime.Now;
            var todayStr = today.ToString("yyyy-MM-dd");
            return todayStr + ".log";
        }
    }
}
