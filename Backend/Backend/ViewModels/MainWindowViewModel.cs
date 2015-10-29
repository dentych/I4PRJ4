using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Fakegenerator;
using Backend.Models;
using Backend.Models.Events;
using Backend.Views;
using Prism.Events;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Categories = faker.Make();
            Categories.Bootstrapper();
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<AddProductWindowLoaded>().Subscribe(AddProductWindowLoaded, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Subscribe(EditProductWindowLoaded, true);
        }

        #region Properties

        public BackendProductCategoryList Categories { get; }
        public int ProductIndex { get; set; } = 0;
        public readonly IEventAggregator Aggregator;
        private readonly FakeMaker faker = new FakeMaker(); // Debug only

        #endregion

        #region Windows

        private void NewAddProductWindow()
        {
            var window = new AddProductWindow();
            window.ShowDialog();
        }

        private void NewEditProductWindow()
        {
            var window = new EditProductWindow();
            window.ShowDialog();
        }

        #endregion

        #region Eventshit

        public void AddProductWindowLoaded(bool b)
        {
            Aggregator.GetEvent<CategoryListUpdated>().Publish(Categories);
        }

        public void EditProductWindowLoaded(bool b)
        {
            var details = new EditProductParameters
            {
                cats = Categories,
                currentCatIndex = Categories.CurrentIndex,
                CurrentCategory = Categories[Categories.CurrentIndex],
                product = Categories.CurrentProductList[ProductIndex] // FIXME: Skal tage markeret produkt.
            };

            Aggregator.GetEvent<NewEditProductData>().Publish(details);
        }

        #endregion

        #region Commands

        /* Add Product */
        private ICommand _openAddProductWindowCommand;

        public ICommand OpenAddProductWindowCommand
        {
            get
            {
                return _openAddProductWindowCommand ??
                       (_openAddProductWindowCommand = new RelayCommand(NewAddProductWindow));
            }
        }

        /* Edit Product */
        private ICommand _openEditProductWindowCommand;

        public ICommand OpenEditProductWindowCommand
        {
            get
            {
                return _openEditProductWindowCommand ??
                       (_openEditProductWindowCommand = new RelayCommand(NewEditProductWindow));
            }
        }


        /* Settings dialog */
        private ICommand _openSettingsDialog;

        public ICommand OpenSettingsDialog
        {
            get { return _openSettingsDialog ?? (_openSettingsDialog = new RelayCommand(OpenSettingsDialogWindow)); }
        }

        private void OpenSettingsDialogWindow()
        {
            var dialog = new SettingsDialog();
            dialog.ShowDialog();
        }

        #endregion
    }
}