using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{

    public class AddCategoryViewModel
    {
        public IEventAggregator Aggregator { get; set; }
        public IModelHandler Handler { get; set; }
        public IError ErrorPrinter { get; set; } 
        public BackendProductCategoryList Categories { get; set; } 
        public  BackendProductCategory Category { get; set; } 

        public AddCategoryViewModel()
        {
            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);

            Aggregator = SingleEventAggregator.Aggregator;
            Handler = new ModelHandler(new PrjProtokol(), new Client());
            ErrorPrinter = new Error();
            Category = new BackendProductCategory();

        }

        
        public bool Valid()
        {
            return !string.IsNullOrEmpty(Category.BName);
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


        private bool Exists(BackendProductCategory editedProduct)
        {
            foreach (var cat in Categories)
            {
                if (cat.BName == editedProduct.BName)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddCategory()
        {
          
            if(!Exists(Category))
                Handler.AddCategory(Category);
            else ErrorPrinter.StdErr("Kategorien eksisterer allerede.");

            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();
        }

        #endregion
    }


}
