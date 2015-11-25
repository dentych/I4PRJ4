using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Automation.Peers;
using SharedLib.Models;

namespace KasseApparat
{
    /// <summary>
    /// Klasse der st�r for at styre en liste med knapper. Denne liste symbolisere
    /// en side af knapper p� gr�nsefladen. Grunden til at denne klasse er tom og
    /// ikke har nogen r�el funktionalitet er, at det kunne v�re rigtig rart at have
    /// hvis der i fremtiden skulle tilf�jes yderligere funktionalitet til hver
    /// side af knapper.
    /// </summary>
    public class ProductButtonList : ObservableCollection<IButtonContent>
    {
        public ProductButtonList()
        {
            
        }
    }
}