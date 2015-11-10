using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Backend.Brains;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
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
        public string OldName { get; set; }
        public int OldId { get; set; }

        public EditCategoryViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditCategoryData>().Subscribe(SetCategoryData, true);
            Aggregator.GetEvent<EditCategoryWindowLoaded>().Publish(true);
            ProductCategoryEdited.ProductCategoryId = OldId;
        }

        public void SetCategoryData(EditCategoryParms p)
        {
            OldName = p.Name;
            OldId = p.Id;
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
            if (OldName != "")
                return true;
            return false;
        }

        private void SaveCategory()
        {
            if(!Exists(ProductCategoryEdited))
                Handler.EditCategory(ProductCategoryEdited); // Burde måske også have OldName med?
            else new Error().StdErr("donnish ffs");
        }

        private bool Exists(BackendProductCategory editedProduct)
        {
            foreach (var cat in AllCats)
            {
                if (ProductCategoryEdited.BName == editedProduct.BName)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
