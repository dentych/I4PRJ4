using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Backend.Brains;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{

    public class AddCategoryViewModel
    {
        public IEventAggregator Aggregator { get; set; } = SingleEventAggregator.Aggregator;
        public IAddCategory IAC { get; set; } = new AddCategory();
        public IError ErrorPrinter { get; set; } = new Error();
        public BackendProductCategoryList Categories { get; set; } 
        public  BackendProductCategory Category { get; set; } = new BackendProductCategory();

        public AddCategoryViewModel()
        {
            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);
        }

        
        public bool Valid()
        {
            return !string.IsNullOrEmpty(Category.Name);
        }

        public void CategoryListUpdated(BackendProductCategoryList updatedList)
        {
            Categories = updatedList;
        }


        #region Commands

        /* Add Product */
        private ICommand _addCaegorytCommand;

        public ICommand AddCategoryCommand
        {
            get { return _addCaegorytCommand ?? (_addCaegorytCommand = new RelayCommand(AddCategory, Valid)); }
        }

        private void AddCategory()
        {
            bool alreadyexist = false;

            foreach (var oldcat in Categories)
            {
                if (oldcat.Name == Category.Name)
                {
                    alreadyexist = true;
                }
            }
            if(!alreadyexist)
                IAC.CreateCategory(Category);
            else ErrorPrinter.StdErr("Kategorien eksisterer allerede.");
        }

        #endregion
    }


}
