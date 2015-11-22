using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Prism.Events;

namespace Backend.ViewModels
{
    /// <summary>
    /// Viewmodel for the delete category window.
    /// </summary>
    public class DeleteCategoryViewModel
    {
        #region Properties
        public IEventAggregator Aggregator;
        public BackendProductCategoryList Categories { get; set; }
        public IModelHandler ModelHandler { get; set; }
        private int SelectedIndex { get; set; } // Den der skal slettes (INDEX!!!)
        public int MoveToCategoryId { get; set; } // den der skal flyttes til, index bindex til den her.
        #endregion
        
        /// <summary>
        /// Set up events to communicate from and to mainwindow viewmodel.
        /// </summary>
        public DeleteCategoryViewModel()
        {
            MoveToCategoryId = 0;
            ModelHandler = new ModelHandler(new PrjProtokol(), new Client());
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewDeleteCategoryData>().Subscribe(GetData, true);
            Aggregator.GetEvent<DeleteCategoryWindowLoaded>().Publish(true);
        }

        /// <summary>
        /// Called when the mainwindow viewmodel sends data to this viewmodel.
        /// </summary>
        /// <param name="parms"></param>
        public void GetData(DeleteCategoryParms parms)
        {
            Categories = parms.cats;
            SelectedIndex = parms.ToDelteIndex;
        }

        #region Commands
        /// <summary>
        /// Command to move products in the category to another category.
        /// </summary>
        private ICommand _moveCommand;
        public ICommand MoveCommand
        {
            get { return _moveCommand ?? (_moveCommand = new RelayCommand(MoveProducts, MoveValid)); }
        }

        /// <summary>
        /// Command for deleting the empty category.
        /// </summary>
        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteCategory, DeleteValid)); }
        }

        /// <summary>
        /// Checks whether the deletion is valid. If it's not valid,
        /// the DeleteCommand will be disabled (and button grayed out in GUI).
        /// The deletion is valid if the current product list is empty.
        /// </summary>
        /// <returns>True if deletion is okay, otherwise false.</returns>
        private bool DeleteValid()
        {
            return (Categories.CurrentProductList.Count == 0);
        }

        /// <summary>
        /// Called when the "DeleteCommand" command is executed.
        /// </summary>
        private void DeleteCategory()
        {
            // Create and send delete category command to central server.
            ModelHandler.DeleteCategory(Categories[SelectedIndex]);
            Application.Current.Windows[Application.Current.Windows.Count - 1].Close();

        }

        /// <summary>
        /// Checks if moving products can be done. This is done by checking if
        /// the category to move to is different than the current category AND if
        /// the current category contains any products.
        /// </summary>
        /// <returns>True if the move is valid, otherwise false.</returns>
        private bool MoveValid()
        {
            return (SelectedIndex != MoveToCategoryId) && (Categories[SelectedIndex].Products.Count > 0);
        }

        /// <summary>
        /// Called when the move products command is executed. It will simply create
        /// and send a multitude of Edit product commands to the Central Server.
        /// </summary>
        private void MoveProducts()
        {
            ModelHandler.MoveProductsInCategory(Categories[SelectedIndex], Categories[MoveToCategoryId].ProductCategoryId);
        }

        #endregion
    }
}
