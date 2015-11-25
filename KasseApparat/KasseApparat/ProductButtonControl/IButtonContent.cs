using System.ComponentModel;
using System.Windows.Input;
using SharedLib.Models;

namespace KasseApparat
{
    public interface IButtonContent
    {
        ICommand AddCommand { get; }
        string Name { get; set; }
        string Price { get; set; }
        Product Product { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}