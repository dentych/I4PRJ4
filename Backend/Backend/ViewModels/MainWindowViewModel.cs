using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Views;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {

        #region Commands

#if DEBUG
        public bool IsCalled = false;
#endif

        /* Add Product */
        ICommand _openAddProductWindowCommand;
        public ICommand OpenAddProductWindowCommand
        {
            get { return _openAddProductWindowCommand ?? (_openAddProductWindowCommand = new RelayCommand(NewAddProductWindow)); }
        }

        private void NewAddProductWindow()
        {
#if DEBUG
        IsCalled = true;
#endif
        var window = new AddProductWindow();
            window.ShowDialog();
        

        }

#endregion

    }
}
