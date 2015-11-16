﻿using System;
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
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Prism.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{
    public class EditCategoryViewModel
    {
        public IEventAggregator Aggregator;
        public BackendProductCategory ProductCategoryEdited { get; set; } = new BackendProductCategory();
        public BackendProductCategoryList Categories { get; set; }
        public IModelHandler Handler { get; set; } = new ModelHandler(new PrjProtokol(), new Client());
        public BackendProductCategoryList AllCats;

        public EditCategoryViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditCategoryData>().Subscribe(SetCategoryData, true);
            Aggregator.GetEvent<EditCategoryWindowLoaded>().Publish(true);
        }

        public void SetCategoryData(EditCategoryParms p)
        {
            ProductCategoryEdited.Name = p.Name;
            ProductCategoryEdited.ProductCategoryId = p.Id;
            AllCats = p.cats;
        }


        #region Commands

        private ICommand _saveCategoryCommand;
        public ICommand SaveCategoryCommand
        {
            get { return _saveCategoryCommand ?? (_saveCategoryCommand = new RelayCommand(SaveCategory, Valid)); }
        }

        private bool Valid() 
        {
            if (ProductCategoryEdited.Name != "")
                return true;
            return false;
        }

        private void SaveCategory()
        {
            if(!Exists())
                Handler.EditCategory(ProductCategoryEdited); // Burde måske også have OldName med?
            else new Error().StdErr("donnish ffs");
        }

        private bool Exists()
        {
            foreach (var cat in AllCats)
            {
                if (cat.Name == ProductCategoryEdited.Name)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
