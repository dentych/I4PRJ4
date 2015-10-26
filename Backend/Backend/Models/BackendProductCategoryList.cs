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

namespace Backend.Models
{
    public class BackendProductCategoryList : ObservableCollection<BackendProductCategory>, INotifyPropertyChanged
    {
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


        protected override event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName]string propName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
