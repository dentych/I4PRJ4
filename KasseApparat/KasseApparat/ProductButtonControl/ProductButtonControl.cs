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
        private readonly ProductList _productList;
        private List<ProductButtonList> _PageList;
        private ShoppingList _shopList;
#endregion

        public ProductButtonControl()
        {
            _productList = new ProductList(); // product category skal oprette denne plz fix
            //Ex. Brug ProductCategoryList.pl - ProductCategory oprettes statisk i xaml
            _PageList = new List<ProductButtonList>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            Update();
        }

        //Updates the productbuttons, with the products contained in the database
        public void Update()
        {
            _productList.Update(); //Going to be deleted plz fix - update bliver en func for ProductCategoryList
            CalculateTotalpage();
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
                    if (_productList.Count > i)
                    {
                        _PageList[pages].Add(new ButtonContent(_productList[i]));
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
            if ((_productList.Count%12) == 0)
            {
                _totalPages = _productList.Count/12;
            }
            else
            {
                _totalPages = (_productList.Count/12)+1;
            }
        }

        public void addItem(int indexItem)
        {
            if (_shopList.Any(x => x.Name == _PageList[_currentPage-1][indexItem].Name))
            {
                _shopList.IncrementQuantity(_shopList.IndexOf(_shopList.Where(x => x.Name == _PageList[_currentPage - 1][indexItem].Name).Single()));
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_PageList[CurrentPages-1][indexItem].Product, 1));
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
