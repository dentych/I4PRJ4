using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Fakegenerator;
using Backend.Models;
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
            Aggregator.GetEvent<Models.Events.AddProductWindowLoaded>().Subscribe(AddProductWindowLoaded,true);
           
        }

        #region Properties

        public BackendProductCategoryList Categories { get; }
        public readonly IEventAggregator Aggregator;
        private readonly FakeMaker faker = new FakeMaker(); // Debug only


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

        private void NewAddProductWindow()
        {
            var window = new AddProductWindow();
            window.ShowDialog();
        }

        public void AddProductWindowLoaded(bool b)
        {
            Aggregator.GetEvent<Models.Events.CategoryListUpdated>().Publish(Categories);

        }

        /* Settings dialog */
        ICommand _openSettingsDialog;
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