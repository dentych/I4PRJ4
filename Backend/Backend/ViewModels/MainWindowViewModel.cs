using System.Windows.Input;
using Backend.Dependencies;
using Backend.Views;
using Backend.Models;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ProductList.GetCatalogue();
        }

        #region Properties
        private BackendProductList _productList = new BackendProductList();

        public BackendProductList ProductList
        {
            get { return _productList; }
        }
        #endregion

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
