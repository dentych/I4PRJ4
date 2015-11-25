using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Automation.Peers;
using SharedLib.Models;

namespace KasseApparat
{
    /// <summary>
    /// Klasse der står for at styre en liste med knapper. Denne liste symbolisere
    /// en side af knapper på grænsefladen. Grunden til at denne klasse er tom og
    /// ikke har nogen réel funktionalitet er, at det kunne være rigtig rart at have
    /// hvis der i fremtiden skulle tilføjes yderligere funktionalitet til hver
    /// side af knapper.
    /// </summary>
    public class ProductButtonList : ObservableCollection<IButtonContent>
    {
        public ProductButtonList()
        {
            
        }
    }
}