using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{
    /// <summary>
    /// Viewmodel for the add category window.
    /// </summary>
    public class AddCategoryViewModel
    {
        public IEventAggregator Aggregator { get; set; } 
        public IModelHandler Handler { get; set; } 
        public IError ErrorPrinter { get; set; } 
        public BackendProductCategoryList Categories { get; set; } 
        public  BackendProductCategory Category { get; set; } 

        public AddCategoryViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Handler = new ModelHandler(new PrjProtokol(), new Client());
            ErrorPrinter = new Error();
            Category = new BackendProductCategory();

            Aggregator.GetEvent<CategoryListUpdated>().Subscribe(CategoryListUpdated);
            Aggregator.GetEvent<AddProductWindowLoaded>().Publish(true);
            
        }

        /// <summary>
        /// Check if the category name is correctly filled.
        /// To be correctly filled out, the category name CAN NOT be empty.
        /// </summary>
        /// <returns>True if the category name has been filled out, false if it's empty.</returns>
        public bool Valid()
        {
            return !string.IsNullOrEmpty(Category.BName);
        }

        /// <summary>
        /// Called when the event from mainwindow viewmodel is raised.
        /// </summary>
        /// <param name="updatedList">A reference to the category list from mainwindow viewmodel.</param>
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

        /// <summary>
        /// Check if a category with the same name already exists.
        /// </summary>
        /// <param name="editedProduct">The category to check for.</param>
        /// <returns>True if there is a category with the same name, otherwise false.</returns>
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

        /// <summary>
        /// Called when the button to create the category is pressed.
        /// </summary>
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
