using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;

namespace Backend.ViewModels
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            Product = new BackendProduct();
            Err = new Error();


            IAP = new AddProductCB(new PrjProtokol(), new Client());
                // Der skal nogle settings til her..
        }

        public BackendProduct Product { get; set; }
        public IAddProduct IAP { get; set; }
        public IError Err {set; get; }

        public bool Valid()
        {
            if (string.IsNullOrEmpty(Product.BName) || Product.BPrice < 0 ||
                string.IsNullOrWhiteSpace(Product.BProductNumber))
            {
                return false;
            }
            return true;
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