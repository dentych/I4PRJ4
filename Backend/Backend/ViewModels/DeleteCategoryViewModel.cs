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
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Prism.Events;

namespace Backend.ViewModels
{
    public class DeleteCategoryViewModel
    {
        #region Properties
        public IEventAggregator Aggregator;
        public BackendProductCategoryList Categories { get; set; }
        public IModelHandler ModelHandler { get; set; } = new ModelHandler(new PrjProtokol(), new Client());
        private int SelectedIndex { get; set; } // Den der skal slettes (INDEX!!!)
        public int MoveToCategoryId { get; set; } = 0; // den der skal flyttes til, index bindex til den her.
        #endregion

        public DeleteCategoryViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewDeleteCategoryData>().Subscribe(GetData, true);
            Aggregator.GetEvent<DeleteCategoryWindowLoaded>().Publish(true);
        }

        public void GetData(DeleteCategoryParms parms)
        {
            Categories = parms.cats;
            SelectedIndex = parms.ToDelteIndex;
        }

        #region Commands

        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get { return _moveCommand ?? (_moveCommand = new RelayCommand(MoveProducts, MoveValid)); }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteCategory, DeleteValid)); }
        }

        private bool DeleteValid()
        {
            return (Categories.CurrentProductList.Count == 0);
        }

        private void DeleteCategory()
        {
            ModelHandler.DeleteCategory(Categories[SelectedIndex]);
        }

        private bool MoveValid()
        {
            return (SelectedIndex != MoveToCategoryId) && (Categories[SelectedIndex].Products.Count > 0);
        }

        private void MoveProducts()
        {
            ModelHandler.MoveProductsInCategory(Categories[SelectedIndex], Categories[MoveToCategoryId].ProductCategoryId);
        }

        #endregion
    }
}
