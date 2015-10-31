using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Events;
using Backend.Views;
using Prism.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{
    class EditProductViewModel
    {
        public IEventAggregator Aggregator;
        public Product ProductToEdit { get; set; }
        public BackendProductCategory ProductCategory { get; set; }
        public BackendProduct EditedProduct { get; set; } = new BackendProduct();
        public BackendProductCategoryList Categories { get; set; }
        public int currentCatIndex { get; set;  }

        public EditProductViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditProductData>().Subscribe(ProductDataToEdit, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Publish(true);

        }

        public void ProductDataToEdit(EditProductParameters details)
        {
            ProductToEdit = details.product;
            ProductCategory = details.CurrentCategory;
            Categories = details.cats;
            currentCatIndex = details.currentCatIndex;

        }

        #region Commands
        private ICommand _addProductCategoryCommand;
        private ICommand _saveProductCommand;


        public ICommand AddCategoryCommand
        {
            get { return _addProductCategoryCommand ?? (_addProductCategoryCommand = new RelayCommand(NewProductCategory)); }
        }

        public ICommand SaveProductCommand
        {
            get { return _saveProductCommand ?? (_saveProductCommand = new RelayCommand(SaveProduct,Valid)); }
        }


        private void SaveProduct()
        {
            // Save the product
            MessageBox.Show("Test");
        }

        private void NewProductCategory()
        {
            var dialog = new AddCategoryWindow();
            dialog.ShowDialog();

        }

        public bool Valid()
        {
            if (string.IsNullOrEmpty(EditedProduct.Name) || EditedProduct.Price < 0 ||
                string.IsNullOrWhiteSpace(EditedProduct.ProductNumber))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
