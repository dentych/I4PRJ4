﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{

    public class AddCategoryViewModel
    {
        public IEventAggregator Aggregator { get; set; } = SingleEventAggregator.Aggregator;
        public IModelHandler Handler { get; set; } = new ModelHandler(new PrjProtokol(), new Client());
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

        private void AddCategory()
        {
            bool alreadyexist = false;

            foreach (var oldcat in Categories)
            {
                if (oldcat.BName == Category.BName)
                {
                    alreadyexist = true;
                }
            }
            if(!alreadyexist)
                Handler.AddCategory(Category);
            else ErrorPrinter.StdErr("Kategorien eksisterer allerede.");
        }

        #endregion
    }


}