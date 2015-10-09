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
        public enum PageChange
        {
            Prevpage,
            Nextpage,
            Currentpage
        }

        private int _totalPages = 0;
        private int _currentPage = 1;
        private readonly ProductList _productList;
        private List<List<Product>> _PageList;
        private List<Product> _products; 
        private ShoppingList _shopList;
#endregion

        public ProductButtonControl()
        {
            _productList = new ProductList();
            _PageList = new List<List<Product>>();
            _products = new List<Product>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            Update();
        }

        public void Update()
        {
            _productList.Update();
            CalculateTotalpage();
            createPageList();
            SetButtons(1, PageChange.Currentpage);
            
        }

        void createPageList()
        {
            _PageList.Clear();

            int pages = 0;
            int i = 0;

            while (pages < _totalPages)
            {
                _PageList.Add(new List<Product>());

                for (int index = i; i < (index+12); i++)
                {
                    if (_productList.Count > i)
                    {
                        _PageList[pages].Add(_productList[i]);
                    }
                    else
                    {
                        _PageList[pages].Add(new Product());
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

        public void SetButtons(int page, PageChange Change)
        {
            if (Change == PageChange.Nextpage)
            {
                _products = _PageList[page];
                _currentPage++;
            }
            else if (Change == PageChange.Currentpage)
            {
                _products = _PageList[page - 1];
            }
            else
            {
                _products = _PageList[page - 2];
                _currentPage--;
            }
        }

        public void addItem(int indexItem)
        {
            if (_shopList.Any(x => x.Name == _products[indexItem].Name))
            {
                _shopList.IncrementQuantity(_shopList.IndexOf(_shopList.Where(x => x.Name == _products[indexItem].Name).Single()));
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[indexItem], 1));
            }
        }

#region Commands
        ICommand _ButtonPrevClick;
        public ICommand PrevCommand { get { return _ButtonPrevClick ?? (_ButtonPrevClick = new RelayCommand(PrevCommandExecute, PrevCommandCanExecute)); } }

        private void PrevCommandExecute()
        {
            SetButtons(_currentPage, PageChange.Prevpage);
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
            SetButtons(_currentPage, PageChange.Nextpage);
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

        public string productButton1Name
        {
            get
            {
                return _products[0].Name;
            }
        }

        public string productButton1Pris
        {
            get
            {
                if (_products[0].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton01.IsEnabled = true;
                    return _products[0].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton01.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton2Name
        {
            get
            {
                return _products[1].Name; 
                
            }
        }

        public string productButton2Pris
        {
            get
            {
                if (_products[1].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton02.IsEnabled = true;
                    return _products[1].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton02.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton3Name
        {
            get
            {
                return _products[2].Name; 
                
            }
        }

        public string productButton3Pris
        {
            get
            {
                if (_products[2].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton03.IsEnabled = true;
                    return _products[2].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton03.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton4Name
        {
            get
            {
                return _products[3].Name; 
                
            }
        }

        public string productButton4Pris
        {
            get
            {
                if (_products[3].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton04.IsEnabled = true;
                    return _products[3].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton04.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton5Name
        {
            get
            {
                return _products[4].Name; 
                
            }
        }

        public string productButton5Pris
        {
            get
            {
                if (_products[4].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton05.IsEnabled = true;
                    return _products[4].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton05.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton6Name
        {
            get
            {
                return _products[5].Name; 
                
            }
        }

        public string productButton6Pris
        {
            get
            {
                if (_products[5].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton06.IsEnabled = true;
                    return _products[5].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton06.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton7Name
        {
            get
            {
                return _products[6].Name; 
                
            }
        }

        public string productButton7Pris
        {
            get
            {
                if (_products[6].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton07.IsEnabled = true;
                    return _products[6].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton07.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton8Name
        {
            get
            {
                return _products[7].Name; 
                
            }
        }

        public string productButton8Pris
        {
            get
            {
                if (_products[7].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton08.IsEnabled = true;
                    return _products[7].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton08.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton9Name
        {
            get
            {
                return _products[8].Name; 
                
            }
        }

        public string productButton9Pris
        {
            get
            {
                if (_products[8].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton09.IsEnabled = true;
                    return _products[8].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton09.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton10Name
        {
            get
            {
                return _products[9].Name; 
                
            }
        }

        public string productButton10Pris
        {
            get
            {
                if (_products[9].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton10.IsEnabled = true;
                    return _products[9].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton10.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton11Name
        {
            get
            {
                return _products[10].Name; 
                
            }
        }

        public string productButton11Pris
        {
            get
            {
                if (_products[10].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton11.IsEnabled = true;
                    return _products[10].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton11.IsEnabled = false;
                    return "";
                }

            }
        }

        public string productButton12Name
        {
            get
            {
                return _products[11].Name;
                
            }
        }

        public string productButton12Pris
        {
            get
            {
                if (_products[11].Name != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton12.IsEnabled = true;
                    return _products[11].Price + "kr. ";
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProductButton12.IsEnabled = false;
                    return "";
                }

            }
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
