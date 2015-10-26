using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Views;
using SharedLib.Models;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {

        public MainWindowViewModel()
        {
            //ProductList.GetCatalogue();


            /* Opret fake produkter */
            for (var i = 0; i < 10; i++)
            {
                var newCategory = new BackendProductCategory
                {
                    Name = "Category " + (i + 1),
                    Products = new List<Product>()
                };
                for (var x = 0; x < 10; x++)
                {
                    var tmpProduct = new BackendProduct
                    {
                        BName = "Name " + i,
                        BPrice = (7*x)/2,
                        BProductNumber = "ABC" + i
                    };
                    newCategory.Products.Add(tmpProduct);
                }
                Categories.Add(newCategory);
            }

            
        }

        #region Properties

        
        public BackendProductCategoryList Categories { get; } = new BackendProductCategoryList();
        public List<Product> ProductList { get; set; } = new List<Product>();




        #endregion

        #region Commands

#if DEBUG
        public bool IsCalled;
#endif

        /* Add Product */
        private ICommand _openAddProductWindowCommand;

        public ICommand OpenAddProductWindowCommand
        {
            get
            {
                return _openAddProductWindowCommand ??
                       (_openAddProductWindowCommand = new RelayCommand(NewAddProductWindow));
            }
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