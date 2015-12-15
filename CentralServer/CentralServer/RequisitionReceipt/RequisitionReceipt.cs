using System;
using System.IO;
using System.Text;
using SharedLib.Models;

namespace CentralServer.RequisitionReceipt
{
    /// <summary>
    /// Enables to write details about a purchase to a file.
    /// The file chosen to write to is named according to the current date.
    /// </summary>
    public class RequisitionReceipt : IRequisitionReceipt
    {
        /// <summary>
        /// Write details about a purchase to the requisition receipt
        /// </summary>
        /// <param name="purchase">The purchase to get details from</param>
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

        /// <summary>
        /// Gets path to file to write to according to current date.
        /// </summary>
        /// <returns>Filepath</returns>
        private string GetFilePath()
        {
            var today = DateTime.Now;
            var todayStr = today.ToString("yyyy-MM-dd");
            return todayStr + ".log";
        }
    }
}
