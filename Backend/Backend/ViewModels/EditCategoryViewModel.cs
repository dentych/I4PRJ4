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
        public string Oldname { get; set; }
        public int Oldid { get; set; }

        public EditCategoryViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditCategoryData>().Subscribe(SetCategoryData, true);
            Aggregator.GetEvent<EditCategoryWindowLoaded>().Publish(true);
            ProductCategoryEdited.ProductCategoryId = Oldid;
        }

        public void SetCategoryData(EditCategoryParms p)
        {
            Oldname = p.Name;
            Oldid = p.Id;
        }


        #region Commands

        private ICommand _saveCategoryCommand;
        public ICommand SaveCategoryCommand
        {
            get { return _saveCategoryCommand ?? (_saveCategoryCommand = new RelayCommand(SaveCategory, Valid)); }
        }

        private bool Valid()
        {
            if (Oldname != "")
                return true;
            return false;
        }

        private void SaveCategory()
        {
            Handler.EditCategory(ProductCategoryEdited); // Burde måske også have OldName med?
        }
        #endregion
    }
}
