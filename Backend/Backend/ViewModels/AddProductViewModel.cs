using System;
using System.ComponentModel.Design;
using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;

namespace Backend.ViewModels
{
    internal class AddProductViewModel
    {
        public AddProductViewModel()
        {
            Product = new BackendProduct();
        }

        public BackendProduct Product { get; set; }

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
        ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get { return _addProductCommand ?? (_addProductCommand = new RelayCommand(AddProduct, Valid)); }
        }

        private void AddProduct()
        {
            try
            {
                var ap = new AddProductCB(new PrjProtokol(), new Client("127.0.0.1", 9000));
                ap.CreateProduct(Product);

            }
            catch (Exception e)
            {

                var err = new Error();
                err.StdErr("Error " + e);
            }

        }

        #endregion


       
    }
}