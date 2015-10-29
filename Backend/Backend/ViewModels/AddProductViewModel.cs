using System.Threading;
using System.Windows;
using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
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
            IAP = new AddProductCB(new PrjProtokol(), new Client());
            Aggregator = SingleEventAggregator.Aggregator;

            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated, true);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);


        }


        public BackendProductCategoryList Categories { get; set; }
        public BackendProduct Product { get; set; }
        public IAddProduct IAP { get; set; }
        public IError Err { set; get; }

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
            IAP.CreateProduct(Product);
        }

        #endregion
    }
}