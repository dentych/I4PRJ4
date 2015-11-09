using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharedLib.Models;
using System.Threading;

namespace Backend.Models
{
    public class BackendProductCategoryList : ObservableCollection<BackendProductCategory>, INotifyPropertyChanged
    {
        #region Properties & vars
        private Mutex _mutex = new Mutex();

        private List<Product> _currentProductList;
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
        public void Bootstrapper()
        {
            if (this[0].Products != null)
            {
                CurrentProductList = this[0].Products;
            }
        }

        private int _currentIndex = 0;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;
                CurrentProductList = this[_currentIndex].Products;
                Notify();
            }
        }

        public BackendProductCategory GetListByCateogry(int categoryId)
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

        public new void Add(BackendProductCategory category)
        {
            _mutex.WaitOne();

            base.Add(category);

            _mutex.ReleaseMutex();
        }

        public new void RemoveAt(int index)
        {
            _mutex.WaitOne();

            base.RemoveAt(index);

            _mutex.ReleaseMutex();
        }

        public void UpdateCurrentProducts()
        {
            List<Product> tmp = CurrentProductList;
            CurrentProductList = null;
            CurrentProductList = tmp;
        }
        #endregion

        #region Events
        protected override event PropertyChangedEventHandler PropertyChanged;

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
