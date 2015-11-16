using System.Windows;
using System.Windows.Input;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Prism.Events;

namespace Backend.ViewModels
{
    public class AddProductViewModel
    {
        public IEventAggregator Aggregator;
        

        public AddProductViewModel()
        {
            Product = new BackendProduct();
            Err = new Error();
            ModelHandler = new ModelHandler(new PrjProtokol(), new Client());
            Aggregator = SingleEventAggregator.Aggregator;

            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated, true);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);

            if (Categories.Count > 0)
            {
                SelectedCategory = Categories[0];
            }
            
        }

        public BackendProductCategoryList Categories { get; set; }
        public BackendProduct Product { get; set; }
        public IModelHandler ModelHandler { get; set; }
        public IError Err { set; get; }
        public BackendProductCategory SelectedCategory { get; set; } = new BackendProductCategory();


        public bool Valid()
        {
            if (string.IsNullOrEmpty(Product.BName) || Product.BPrice < 0 ||
                string.IsNullOrWhiteSpace(Product.BProductNumber))
            {
                return false;
            }
            return true;
        }

        public void CategoryListUpdated(BackendProductCategoryList updatedList)
        {
            Categories = updatedList;
        }

        #region Commands

        /* Add Product */
        private ICommand _addProductCommand;

        public ICommand AddProductCommand
        {
            get { return _addProductCommand ?? (_addProductCommand = new RelayCommand(AddProduct, Valid)); }
        }

        private void AddProduct()
        {
            Product.ProductCategoryId = SelectedCategory.ProductCategoryId;
            if (!Exists(Product))
                ModelHandler.CreateProduct(Product);
            else new Error().StdErr("Produktet eksisterer allerede.");
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();

        }

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