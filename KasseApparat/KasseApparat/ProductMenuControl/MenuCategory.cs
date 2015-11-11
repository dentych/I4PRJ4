using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using SharedLib.Models;

namespace KasseApparat.ProductMenuControl
{
    public class MenuCategory : MenuItem
    {
        private List<Product> ProductList;
        private ProductButtonControl _productButtonControl;

        public MenuCategory(string name, List<Product> products)
        {
            ProductList = products;
            _productButtonControl = (ProductButtonControl)Application.Current.MainWindow.FindResource("ProductButtonControl");

            Command = MenuItemClick;
            Header = name;
            MinHeight = 50;
        }

        ICommand _MenuItemClick;
        public ICommand MenuItemClick { get { return _MenuItemClick ?? (_MenuItemClick = new RelayCommand(MenuItemExecute, MenuItemCanExecute)); } }

        private void MenuItemExecute()
        {
            _productButtonControl.Update(ProductList);
        }

        bool MenuItemCanExecute()
        {
            return true;
        }        
    }
}
