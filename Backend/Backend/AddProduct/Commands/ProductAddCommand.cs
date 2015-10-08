using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Backend.AddProduct.Brains;
using Backend.AddProduct.ViewModels;

namespace Backend.AddProduct.Commands
{
    internal class ProductAddCommand : ICommand
    {

        private AddProductViewModel _ProductVM;

        public ProductAddCommand(AddProductViewModel ProductVM)
        {
            _ProductVM = ProductVM;
        }

#region CommandMembers


        public bool CanExecute(object parameter)
        {
            return _ProductVM.Valid;
        }

        public void Execute(object parameter)
        {
           _ProductVM.AddProduct();
        }

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}
