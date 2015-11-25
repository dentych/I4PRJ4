using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Backend.Views;
using Prism.Events;
using System;

namespace Backend.ViewModels
{
    /// <summary>
    /// Viewmodel for the edit product window.
    /// </summary>
    public class EditProductViewModel
    {
        public IEventAggregator Aggregator;
        public IModelHandler Handler { get; set; } 
        public BackendProductCategory ProductCategory { get; set; }
        public BackendProduct EditedProduct { get; set; } 
        public BackendProductCategoryList Categories { get; set; }
        public int currentCatIndex { get; set;  }
        public IError Err { get; set; }

        public EditProductViewModel()
        {
            Handler = new ModelHandler(new PrjProtokol(), new Client());
            EditedProduct = new BackendProduct();
            Err = new Error();

            // Subscribe to event, and publish a window loaded event, to receive data.
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditProductData>().Subscribe(ProductDataToEdit, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Publish(true);
        }

        /// <summary>
        /// Set the data of EditedProduct to the received data of details.
        /// EditedProduct is data bound to the view.
        /// </summary>
        /// <param name="details">Details about the product to be edited</param>
        public void ProductDataToEdit(EditProductParameters details)
        {
            EditedProduct.BName = details.product.Name;
            EditedProduct.BPrice = details.product.Price;
            EditedProduct.ProductId = details.product.ProductId;
            EditedProduct.BProductNumber = details.product.ProductNumber;
            Categories = details.cats;
            currentCatIndex = details.currentCatIndex;
        }

        #region Commands
        private ICommand _addProductCategoryCommand;
        public ICommand AddCategoryCommand
        {
            get { return _addProductCategoryCommand ?? (_addProductCategoryCommand = new RelayCommand(NewProductCategory)); }
        }

        private ICommand _saveProductCommand;
        public ICommand SaveProductCommand
        {
            get { return _saveProductCommand ?? (_saveProductCommand = new RelayCommand(SaveProduct,Valid)); }
        }

        /// <summary>
        /// Save the product by sending a command to central server.
        /// </summary>
        private void SaveProduct()
        {
            if (!Exists())
            {
                // Set the product category ID of edited product to the current chosen category in the GUI.
                EditedProduct.ProductCategoryId = Categories[currentCatIndex].ProductCategoryId;
                // Generate and send an edit product command to Central Server.
                Handler.EditProduct(EditedProduct);
            }
            else Err.StdErr("Produktet eksisterer allerede.");
            try {
                Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Check whether or not an exact copy of this product exists in any
        /// of the categories.
        /// </summary>
        /// <returns>True if a copy if found, otherwise false.</returns>
        private bool Exists()
        {
            foreach (var cat in Categories)
            {
                foreach (var product in cat.Products)
                {
                    if (product.ProductId == EditedProduct.ProductId) continue;

                    if (EditedProduct.Name == product.Name && EditedProduct.Price == product.Price &&
                        EditedProduct.ProductNumber == product.ProductNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Opens a new window to create a new category.
        /// </summary>
        private void NewProductCategory()
        {
            var dialog = new AddCategoryWindow();
            dialog.ShowDialog();
        }

        /// <summary>
        /// Checks if any of these three conditions are met:
        /// * Product name is null or empty.
        /// * Product price is below 0.
        /// * Product barcode is null or whitespace.
        /// </summary>
        /// <returns>True if any of the conditions are met, otherwise false.</returns>
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
