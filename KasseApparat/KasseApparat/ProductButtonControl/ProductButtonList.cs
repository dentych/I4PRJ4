using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Automation.Peers;
using SharedLib.Models;

namespace KasseApparat
{
    public class ProductButtonList : ObservableCollection<ButtonContent>
    {
        public ProductButtonList() { }

        public ProductButtonList(List<Product> p)
        {
            this.Clear();

            foreach (Product prod in p)
            {
                Add(new ButtonContent(prod.Name, prod.Price.ToString()));
            }
        }
    }
}