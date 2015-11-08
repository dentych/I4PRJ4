using System.Windows;
using System.Windows.Input;
using Backend.Dependencies;
using Backend.Fakegenerator;
using Backend.Models;
using Backend.Models.Events;
using Backend.Views;
using Prism.Events;
using SharedLib.Models;
using Backend.Brains;
using Backend.Communication;
using SharedLib.Protocol.Commands;

namespace Backend.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Categories = faker.Make(); // TODO: REAL SHIT
            Categories.Bootstrapper();
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<AddProductWindowLoaded>().Subscribe(AddProductWindowLoaded, true);
            Aggregator.GetEvent<AddProductWindowLoaded>().Subscribe(AddCategoryLoaded, true);
            Aggregator.GetEvent<EditCategoryWindowLoaded>().Subscribe(EditCategoryLoaded, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Subscribe(EditProductWindowLoaded, true);

            LSC.Listener.OnProductCreated += ProductCreatedHandler;
            LSC.Listener.OnCatalogueDetails += CatalogueDetailsHandler;

        }

        #region DataReaders

        public void ProductCreatedHandler(ProductCreatedCmd product)
        {
            this.Categories.GetListByCateogry("//product.Category").Add(product.GetProduct());
        }

        public void CatalogueDetailsHandler(CatalogueDetailsCmd cmd)
        {
            foreach (var category in cmd.Products) // NO IT IS PRODUCTCATEGORY)
            {
              //  this.Categories.Add(category);
            }
        }


        #endregion

        #region Properties

        public BackendProductCategoryList Categories { get; }
        public int ProductIndex { get; set; } = 0;
        public readonly IEventAggregator Aggregator;
        private readonly FakeMaker faker = new FakeMaker(); // Debug only
        private IModelHandler modelHandler = new ModelHandler(new PrjProtokol(), new Client());

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

        public void AddCategoryLoaded(bool b)
        {
            Aggregator.GetEvent<CategoryListUpdated>().Publish(Categories);
        }

        public void EditCategoryLoaded(bool b)
        {
            EditCategoryParms p = new EditCategoryParms();
            p.Name = Categories[Categories.CurrentIndex].BName;
            p.Id = Categories[Categories.CurrentIndex].ProductCategoryId;
            Aggregator.GetEvent<NewEditCategoryData>().Publish(p);
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

        /* Add category */
        private ICommand _openAddCategoryWindowCommand;

        public ICommand OpenAddCategoryWindowCommand
        {
            get
            {
                return _openAddCategoryWindowCommand ??
                       (_openAddCategoryWindowCommand = new RelayCommand(OpenAddCategoryDialogWindow));
            }
        }

        /* Edit category */
        private ICommand _openEditCategoryWindowCommand;

        public ICommand OpenEditCategoryWindowCommand
        {
            get
            {
                return _openEditCategoryWindowCommand ??
                       (_openEditCategoryWindowCommand = new RelayCommand(OpenEditCategoryDialogWindow));
            }
        }

        /* Settings dialog */
        private ICommand _openSettingsDialog;

        public ICommand OpenSettingsDialog
        {
            get { return _openSettingsDialog ?? (_openSettingsDialog = new RelayCommand(OpenSettingsDialogWindow)); }
        }

        /* Delete product */
        private ICommand _deleteProductCommand;

        public ICommand DeleteProductCommand
        {
            get { return _deleteProductCommand ?? (_deleteProductCommand = new RelayCommand(DeleteProductDialog, () => ProductIndex >= 0)); }
        }

        /* Close main window */
        private ICommand _closeMainWindowCommand;

        public ICommand CloseMainWindowCommand
        {
            get { return _closeMainWindowCommand ?? (_closeMainWindowCommand = new RelayCommand(CloseMainWindow)); }
        }

        private void OpenSettingsDialogWindow()
        {
            var dialog = new SettingsDialog();
            dialog.ShowDialog();
        }

        private void OpenAddCategoryDialogWindow()
        {
            var dialog = new AddCategoryWindow();
            dialog.ShowDialog();
        }

        private void OpenEditCategoryDialogWindow()
        {
            var dialog = new EditCategoryWindow();
            dialog.ShowDialog();
        }

        private void DeleteProductDialog()
        {
            if (ProductIndex > 0)
            {
                // New message box
                var result = MessageBox.Show("Vil du slette det valgte produkt?", "Slet af produkt", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (ProductIndex >= 0)
                    {
                        modelHandler.DeleteProduct(Categories.CurrentProductList[ProductIndex]);
                    }
                }
            }
            else
            {
                MessageBox.Show("Intet produkt valgt.", "Slet af produkt", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            
        }

        private void CloseMainWindow()
        {
            var result = MessageBox.Show("Er du nu HELT sikker?", "Advarsel", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Close();
            }
        }
        #endregion
    }
}