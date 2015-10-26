using System.Windows.Input;
using Backend.Dependencies;
using Backend.Fakegenerator;
using Backend.Models;
using Backend.Views;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Categories = faker.Make();
            Categories.Bootstrapper();
        }

        #region Properties

        public BackendProductCategoryList Categories { get; }
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
            var window = new AddProductWindow(Categories);
            window.ShowDialog();
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