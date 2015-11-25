using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using SharedLib.Models;

namespace KasseApparat
{
    public interface IProductButtonControl
    {
        ProductButtonList CurrentButtonPage { get; }
        int CurrentPages { get; }
        ICommand NextCommand { get; }
        ICommand PrevCommand { get; }
        int TotalPages { get; }

        event PropertyChangedEventHandler PropertyChanged;

        void Update(List<Product> newButtonList);
    }
}