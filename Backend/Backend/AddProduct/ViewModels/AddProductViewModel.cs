using System.Windows.Input;
using Backend.AddProduct.Brains;
using Backend.AddProduct.Commands;
using Backend.AddProduct.Models;
using Backend.Communication;

namespace Backend.AddProduct.ViewModels
{
    internal class AddProductViewModel
    {
        public AddProductViewModel()
        {
            AddCommand = new ProductAddCommand(this);
            Product = new BackendProduct();
        }

        public BackendProduct Product { get; set; }

        public bool Valid
        {
            get
            {
                if (string.IsNullOrEmpty(Product.BName) || Product.BPrice < 0 ||
                    string.IsNullOrWhiteSpace(Product.BProductNumber))
                {
                    return false;
                }
                return true;
            }
        }


        public ICommand AddCommand { get; private set; }

        public void AddProduct()
        {
            var ap = new AddProductCB(new PrjProtokol(), new Client("127.0.0.1", 9000));
            ap.CreateProduct(Product);
        }
    }
}