using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Backend.Communication;
using Backend.Dependencies;
using Backend.Models;
using Backend.Models.Brains;
using Backend.Models.Communication;
using Backend.Models.Datamodels;
using Backend.Models.Events;
using Backend.Models.SocketEvents;
using Backend.Views;
using Prism.Events;
using SharedLib.Sockets;

namespace Backend.ViewModels
{
    /// <summary>
    /// Main window's view model
    /// </summary>
    public class MainWindowViewModel
    {


        #region Properties

        public BackendProductCategoryList Categories { get; } 
        public int ProductIndex { get; set; }
        public readonly IEventAggregator Aggregator;
        //private readonly FakeMaker faker = new FakeMaker(); // Debug only
        private readonly IModelHandler modelHandler;
        private readonly ISocketEventHandlers _ev;
        private readonly SocketConnection conn;
        private bool DBCON;
        public ConnectionString Connection { get; }

        #endregion

        /// <summary>
        /// Set up events for socket communication and viewmodel-viewmodel communication.
        /// </summary>
        public MainWindowViewModel()
        {
            Categories = new BackendProductCategoryList();
            ProductIndex = 0;
            modelHandler = new ModelHandler(new PrjProtokol(), new Client());
            Connection = new ConnectionString();


            // Socket communication events
            conn = LSC.Connection;
            conn.OnConnectionError += ConnectionErrorHandler;
            conn.OnConnectionOpened += ConenctionOpenedHandler;
            conn.OnConnectionClosed += ConnectionClosedHandler;
            //    conn.OnDataRecieved += DataReceivedHandler;

            try
            {
                conn.Connect(Properties.Settings.Default.CSIP, Properties.Settings.Default.CSPort);
            }
            catch (Exception)
            {
                MessageBox.Show("No connection");
            }

            // Event aggregator for sending data between viewmodels
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<AddProductWindowLoaded>().Subscribe(AddProductWindowLoaded, true);
            Aggregator.GetEvent<AddProductWindowLoaded>().Subscribe(AddCategoryLoaded, true);
            Aggregator.GetEvent<EditCategoryWindowLoaded>().Subscribe(EditCategoryLoaded, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Subscribe(EditProductWindowLoaded, true);
            Aggregator.GetEvent<DeleteCategoryWindowLoaded>().Subscribe(DeleteCategoryWindow, true);

            // More socket communication events
            _ev = new SocketEventHandlers(Categories);
            _ev.SubscribeCatalogueDetails();
            _ev.SubscribeProductCreated();
            _ev.SubscribeProductDeleted();
            _ev.SubscribeProductEdited();
            _ev.SubscribeProductCategoryCreated();
            _ev.SubscribeProductCategoryDeleted();
            _ev.SubscribeProductCategoryEdited();

            /* Send anmodning om catalogue
           * SocketEvents getcatalogue bliver invoked
           * Kategoierne bliver lagt ind
           * bootstrapper kørers */
           if(DBCON)
                modelHandler.CatalogueDetails();
            //     var tmp = new FakeMaker();
            //     Categories = tmp.Make();
        }


        #region Windows

        /// <summary>
        /// Opened when the button for add product is pressed in the GUI
        /// </summary>
        private void NewAddProductWindow()
        {
            var window = new AddProductWindow();
            window.ShowDialog();
        }

        /// <summary>
        /// Called when the socket client has opened a connection to the central server.
        /// </summary>
        private void ConenctionOpenedHandler()
        {
            Connection.Connection = "Forbundet"; //TODO DET KLAMME LORT VIRKER IKKE
            DBCON = true;
        }

        /// <summary>
        /// Called if any errors occurs when trying to connect to central server.
        /// </summary>
        /// <param name="e">The exception that has been thrown</param>
        private void ConnectionErrorHandler(SocketException e)
        {
            new Error().StdErr("Connection error:\n" + e);
            ConnectionClosedHandler();
        }

        /// <summary>
        /// Called when the socket connection is closed.
        /// </summary>
        private void ConnectionClosedHandler()
        {
            Connection.Connection = "Ikke forbundet";
            DBCON = false;
        }

        /// <summary>
        /// Opens a new edit product window when the corresponding button is pressed in the GUI.
        /// </summary>
        private void NewEditProductWindow()
        {
            var window = new EditProductWindow();
            window.ShowDialog();
        }

        #endregion

        #region Eventshit

        /*
        These event handlers are called when an event is
        received from another window. Data will then be sent
        back to the window with the requested data.
        This makes viewmodel to viewmodel communication
        possible, so data does not have to be routed through
        the view.
        */

        public void AddProductWindowLoaded(bool b)
        {
            Aggregator.GetEvent<CategoryListUpdated>().Publish(Categories);
        }

        public void AddCategoryLoaded(bool b)
        {
            Aggregator.GetEvent<CategoryListUpdated>().Publish(Categories);
        }

        public void DeleteCategoryWindow(bool b)
        {
            var p = new DeleteCategoryParms
            {
                cats = Categories,
                ToDelteIndex = Categories.CurrentIndex
            };

            Aggregator.GetEvent<NewDeleteCategoryData>().Publish(p);
        }

        public void EditCategoryLoaded(bool b)
        {
            var p = new EditCategoryParms
            {
                Name = Categories[Categories.CurrentIndex].BName,
                Id = Categories[Categories.CurrentIndex].ProductCategoryId,
                cats = Categories
            };
            Aggregator.GetEvent<NewEditCategoryData>().Publish(p);
        }

        public void EditProductWindowLoaded(bool b)
        {
            var details = new EditProductParameters
            {
                cats = Categories,
                currentCatIndex = Categories.CurrentIndex,
                CurrentCategory = Categories[Categories.CurrentIndex],
                product = Categories.CurrentProductList[ProductIndex]
                //TODO: Skal tage markeret produkt. I think is fixed
            };

            Aggregator.GetEvent<NewEditProductData>().Publish(details);
        }

        #endregion

        #region Commands

        /* Valid CED */
        private bool ValidCED()
        {
            return DBCON;
        }

        /* Add Product */
        private ICommand _openAddProductWindowCommand;

        public ICommand OpenAddProductWindowCommand
        {
            get
            {
                return _openAddProductWindowCommand ??
                       (_openAddProductWindowCommand = new RelayCommand(NewAddProductWindow, ValidCED));
            }
        }

        /* Edit Product */
        private ICommand _openEditProductWindowCommand;

        public ICommand OpenEditProductWindowCommand
        {
            get
            {
                return _openEditProductWindowCommand ??
                       (_openEditProductWindowCommand =
                           new RelayCommand(NewEditProductWindow, () => ProductIndex >= 0 && DBCON));
            }
        }

        /* Add category */
        private ICommand _openAddCategoryWindowCommand;

        public ICommand OpenAddCategoryWindowCommand
        {
            get
            {
                return _openAddCategoryWindowCommand ??
                       (_openAddCategoryWindowCommand = new RelayCommand(OpenAddCategoryDialogWindow, ValidCED));
            }
        }

        /* Edit category */
        private ICommand _openEditCategoryWindowCommand;

        public ICommand OpenEditCategoryWindowCommand
        {
            get
            {
                return _openEditCategoryWindowCommand ??
                       (_openEditCategoryWindowCommand = new RelayCommand(OpenEditCategoryDialogWindow, ValidCED));
            }
        }

        /* Delete category */
        private ICommand _openDeleteCategoryWindowCommand;

        public ICommand OpenDeleteCategoryWindowCommand
        {
            get
            {
                return _openDeleteCategoryWindowCommand ??
                       (_openDeleteCategoryWindowCommand = new RelayCommand(OpenDeleteCategoryDialogWindow, ValidCED));
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
            get
            {
                return _deleteProductCommand ??
                       (_deleteProductCommand = new RelayCommand(DeleteProductDialog, () => ProductIndex >= 0 && DBCON));
            }
        }

        /* Close main window */
        private ICommand _closeMainWindowCommand;

        public ICommand CloseMainWindowCommand
        {
            get { return _closeMainWindowCommand ?? (_closeMainWindowCommand = new RelayCommand(CloseMainWindow)); }
        }

        /* Settings dialog */
        private void OpenSettingsDialogWindow()
        {
            var dialog = new SettingsDialog();
            dialog.ShowDialog();
        }

        /* Add category */
        private void OpenAddCategoryDialogWindow()
        {
            var dialog = new AddCategoryWindow();
            dialog.ShowDialog();
        }

        /* Edit category */
        private void OpenEditCategoryDialogWindow()
        {
            var dialog = new EditCategoryWindow();
            dialog.ShowDialog();
        }

        /* Delete category */
        private void OpenDeleteCategoryDialogWindow()
        {
            var dialog = new DeleteCategoryView();
            dialog.ShowDialog();
        }

        /* Delete product */
        private void DeleteProductDialog()
        {
            if (ProductIndex >= 0)
            {
                // New message box
                var result = MessageBox.Show("Vil du slette det valgte produkt?", "Slet af produkt",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

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

        /* Close the main window */
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