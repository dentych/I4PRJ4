using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using SharedLib.Models;
using System.Windows;

namespace Backend.Models.Datamodels
{
    /// <summary>
    /// Category list, containing all the product categories in the system.
    /// </summary>
    public class BackendProductCategoryList : AsyncObservableCollection<BackendProductCategory>, INotifyPropertyChanged
    {
        #region Properties & vars
        private List<Product> _currentProductList;
        /// <summary>
        /// Current product list. This is data bound to the list in the main window.
        /// </summary>
        public List<Product> CurrentProductList
        {
            get { return _currentProductList; }
            set
            {
                _currentProductList = value;
                Notify();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the CurrentProductList to the first product list this object contains.
        /// </summary>
        public void Bootstrapper()
        {
            if (this[0].Products != null)
            {
                CurrentProductList = this[0].Products;
            }
        }

        private int _currentIndex = 0;
        /// <summary>
        /// Current index in the CurrentProductList.
        /// </summary>
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (value >= 0)
                {
                    _currentIndex = value;
                    CurrentProductList = this[_currentIndex].Products;
                    Notify();
                }
            }
        }

        /// <summary>
        /// Get the list corresponding to the given category id.
        /// </summary>
        /// <param name="categoryId">Category ID to get a list for.</param>
        /// <returns>Will return the list for the given ID if one is found. If no list is found, null will be returned.</returns>
        public BackendProductCategory GetListByCategory(int categoryId)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].ProductCategoryId == categoryId)
                {
                    return this[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Much hack. This will invoke Updater on the main thread. This will set the
        /// CurrentProductList to null and then back to its previous value. This causes
        /// the list to be force-updated.
        /// </summary>
        public void UpdateCurrentProducts()
        {
            // Should the Application not be running, or the window is closed, then we call the Updater from the current thread.
            try
            {
                Application.Current.Dispatcher.Invoke(() => Updater());
            }
            catch (Exception)
            {
                Updater();
            }
            
        }

        /// <summary>
        /// Updates the products in the CurrentProductList in the GUI. This is achieved
        /// by setting the CurrentProductList to null and then back to its previous value.
        /// </summary>
        private void Updater()
        {
            List<Product> tmp = CurrentProductList;
            CurrentProductList = null;
            CurrentProductList = tmp;
        }
        #endregion

        #region Events
        protected override event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies of a changed property. This is used for data binding purposes.
        /// </summary>
        /// <param name="propName">The name of the changed property.</param>
        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
