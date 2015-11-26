using System;
using System.Collections.Generic;
using System.IO;
using SharedLib.Models;

namespace KasseApparat
{
    public class FilePrinter: IPrinter
    {
        public void PrintPurchase(List<PurchasedProduct> shoplist)
        {
            DateTime dt = DateTime.Now;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string now = "/" + dt.Month + dt.Day + dt.Year +
                         dt.Hour + dt.Minute + dt.Second + ".txt";

            using (StreamWriter text = File.CreateText(path + now))
            {
                decimal total = 0;
                text.WriteLine("Vare" + "\t" + "Antal" + "\t" + "Total");
                text.WriteLine("-------------------------");
                foreach (var item in shoplist)
                {
                    if (item.Name == "Kontant")
                    {
                        text.WriteLine("-------------------------");
                        text.WriteLine("Total" + "\t\t" + total);
                        text.WriteLine(item.Name + "\t\t" + -item.TotalPrice);
                        text.WriteLine("Retur" + "\t\t" + (-item.TotalPrice - total));
                    }
                    else
                    {
                        total += item.TotalPrice;
                        string i = item.Name + "\t" + item.Quantity + "\t" + item.TotalPrice;
                        text.WriteLine(i);
                    }
                    
                }
            }
        }
    }
}