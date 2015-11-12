using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace KasseApparat
{
    public class ProductButtonControl : INotifyPropertyChanged
    {
#region attributes

        private int _totalPages = 1;
        private int _currentPage = 1;
        private List<Product> _PDL; 
        private List<ProductButtonList> _PageList;
        private ShoppingList _shopList;

        private int PCLindex = 1; //For PCL
#endregion
         
        /* 
         * Ctor: Constructor for klassen.
         */ 
        public ProductButtonControl()
        {
            _PDL = new List<Product>(); //Changed to PCL
            _PageList = new List<ProductButtonList>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            CreatePageList();
        }

        /* 
         * Ctor: Constructor der er skabt for at gøre hele klassen mere testbar.
         
        public ProductButtonControl(ProductList pl, List<ProductButtonList> pbl, ShoppingList sl)
        {
            _productList = pl;
            _PageList = pbl;
            _shopList = sl;

            Update();
        }
        */

        //Updates the productbuttons, with the products contained in the database
        public void Update(List<Product> createList)
        {
            _PDL = createList;
            CalculateTotalpage();
            _currentPage = 1;
            CreatePageList();

            //Notifying for new changes
            Notify(string.Empty);
        }

        //Creates Pagelist
        void CreatePageList()
        {
            _PageList.Clear();

            int pages = 0;
            int i = 0;

            while (pages < _totalPages)
            {
                _PageList.Add(new ProductButtonList());

                for (int index = i; i < (index+12); i++)
                {
                    if (_PDL.Count > i) //PCL changed
                    {
                        _PageList[pages].Add(new ButtonContent(_PDL[i])); //PCL changed
                    }
                    else
                    {
                        _PageList[pages].Add(new ButtonContent("", ""));
                    }
                }
                
                pages++;
            }
        }

        void CalculateTotalpage()
        {
            if ((_PDL.Count %12) == 0)
            {
                _totalPages = _PDL.Count /12;
            }
            else
            {
                _totalPages = (_PDL.Count /12)+1;
            }
        }

#region Commands
        ICommand _ButtonPrevClick;
        public ICommand PrevCommand { get { return _ButtonPrevClick ?? (_ButtonPrevClick = new RelayCommand(PrevCommandExecute, PrevCommandCanExecute)); } }

        private void PrevCommandExecute()
        {
            _currentPage--;
            Notify(string.Empty);
        }

        bool PrevCommandCanExecute()
        {
            if (_currentPage == 1)
                return false;
            else
                return true;
        }

        ICommand _ButtonNextClick;
        public ICommand NextCommand { get { return _ButtonNextClick ?? (_ButtonNextClick = new RelayCommand(NextCommandExecute, NextCommandCanExecute)); } }

        private void NextCommandExecute()
        {
            _currentPage++;
            Notify(string.Empty);
        }

        bool NextCommandCanExecute()
        {
            if (_currentPage == _totalPages)
                return false;
            else
                return true;
        }



#endregion

#region Properties

        public ProductButtonList CurrentButtonPage
        {
            get { return _PageList[_currentPage-1]; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            private set { _totalPages = value; }
        }

        public int CurrentPages
        {
            get { return _currentPage; }
        }

#endregion

        public new event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
