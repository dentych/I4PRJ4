using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Prism.Events;
using System;

namespace Backend.ViewModels
{
    /// <summary>
    /// Viewmodel for the add product window.
    /// </summary>
    public class AddProductViewModel
    {
        public IEventAggregator Aggregator;

        public BackendProductCategoryList Categories { get; set; }
        public BackendProduct Product { get; set; }
        public IModelHandler ModelHandler { get; set; }
        public IError Err { set; get; }
        public BackendProductCategory SelectedCategory { get; set; } 

        /// <summary>
        /// Set up events for viewmodel-viewmodel communication, 
        /// and select a standard category on the category list.
        /// </summary>
        public AddProductViewModel()
        {
            SelectedCategory = new BackendProductCategory();
            Product = new BackendProduct();
            Err = new Error();
            ModelHandler = new ModelHandler(new PrjProtokol(), new Client());
            Aggregator = SingleEventAggregator.Aggregator;

            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated, true);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);
        }

        /// <summary>
        /// Check if all fields for the product is filled out and
        /// doesn't contain illegal values (for example that price is below 0).
        /// </summary>
        /// <returns>True if the product is filled out correctly, otherwise false.</returns>
        public bool Valid()
        {
            if (string.IsNullOrEmpty(Product.BName) || Product.BPrice < 0 ||
                string.IsNullOrWhiteSpace(Product.BProductNumber))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Called when this viewmodel receives the correct event from the mainwindow viewmodel.
        /// </summary>
        /// <param name="updatedList">A variable containing a link to the category list from
        /// the mainwindow viewmodel</param>
        public void CategoryListUpdated(BackendProductCategoryList updatedList)
        {
            Categories = updatedList;

            if (Categories.Count > 0)
            {
                SelectedCategory = Categories[0];
            }
        }

        #region Commands
        /* Add Product */
        private ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get { return _addProductCommand ?? (_addProductCommand = new RelayCommand(AddProduct, Valid)); }
        }

        /// <summary>
        /// Sends a create product command to the Central Server.
        /// </summary>
        private void AddProduct()
        {
            // Set the product's category ID to the currently selected category ID.
            Product.ProductCategoryId = SelectedCategory.ProductCategoryId;
            if (!Exists(Product))
                ModelHandler.CreateProduct(Product);
            else Err.StdErr("Produktet eksisterer allerede.");
            try
            {
                Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
            }
            catch(Exception) { }
        }

        /// <summary>
        /// Check if there is a duplicate of this product in any of the categories.
        /// A duplicate is a product with the same name, price and product number.
        /// The product ID is irrelevant in this context.
        /// </summary>
        /// <param name="editedProduct">Product to check for in the product lists</param>
        /// <returns>True if the product already exists, otherwise false.</returns>
        private bool Exists(BackendProduct editedProduct)
        {
            foreach (var cat in Categories)
            {
                foreach (var product in cat.Products)
                {
                    if (editedProduct.BName == product.Name && editedProduct.BPrice == product.Price &&
                        editedProduct.BProductNumber == product.ProductNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}