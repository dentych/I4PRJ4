using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Windows;

namespace KasseApparat
{
    public class ProductButtonControl
    {
        private int _totalPages = 0;
        private int _currentPage = 1;
        private readonly ProductList _productList;
        private List<Product> _products;
        private ShoppingList _shopList;

        public ProductButtonControl()
        {
            _productList = new ProductList();
            _products = new List<Product>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            for (int i = 0; i < 12; i++)
            {
                if (_productList.Count > i)
                {
                    _products.Add(_productList[i]);  
                }
                else
                {
                    _products.Add(new Product());
                }
                
            }

            Update();
        }

        public void Update()
        {
            _productList.Update();
            SetButtons();
            CalculateTotalpage();
        }

        void CalculateTotalpage()
        {
            if ((_productList.Count%12) == 0)
            {
                TotalPages = _productList.Count/12;
            }
            else
            {
                TotalPages = (_productList.Count/12)+1;
            }
        }

        public void SetButtons()
        {
            /*for (int i = 0; i < 5; i++)
            {
            _products.Add(_productList[i].Name);    
            }*/

            /*for(int i = 0; i < 7; i++)
            {
                _products.Add();
            }*/
        }

        private void ButtonNaming()
        {
            
        }

#region Properties

        public string productButton1Name
        {
            get { return _products[0].Name; }
        }

        public string productButton1Pris
        {
            get { return _products[0].Price + " kr."; }
        }

        public string productButton2Name
        {
            get { return _products[1].Name; }
        }

        public string productButton2Pris
        {
            get { return _products[1].Price + " kr."; }
        }

        public string productButton3Name
        {
            get { return _products[2].Name; }
        }

        public string productButton3Pris
        {
            get { return _products[2].Price + " kr."; }
        }

        public string productButton4Name
        {
            get { return _products[3].Name; }
        }

        public string productButton4Pris
        {
            get { return _products[3].Price + " kr."; }
        }

        public string productButton5Name
        {
            get { return _products[4].Name; }
        }

        public string productButton5Pris
        {
            get { return _products[4].Price + " kr."; }
        }

        public string productButton6Name
        {
            get { return _products[5].Name; }
        }

        public string productButton6Pris
        {
            get { return _products[5].Price + " kr."; }
        }

        public string productButton7Name
        {
            get { return _products[6].Name; }
        }

        public string productButton7Pris
        {
            get { return _products[6].Price + " kr."; }
        }

        public string productButton8Name
        {
            get { return _products[7].Name; }
        }

        public string productButton8Pris
        {
            get { return _products[7].Price + " kr."; }
        }

        public string productButton9Name
        {
            get { return _products[8].Name; }
        }

        public string productButton9Pris
        {
            get { return _products[8].Price + " kr."; }
        }

        public string productButton10Name
        {
            get { return _products[9].Name; }
        }

        public string productButton10Pris
        {
            get { return _products[9].Price + " kr."; }
        }

        public string productButton11Name
        {
            get { return _products[10].Name; }
        }

        public string productButton11Pris
        {
            get { return _products[10].Price + " kr."; }
        }

        public string productButton12Name
        {
            get { return _products[11].Name; }
        }

        public string productButton12Pris
        {
            get { return _products[11].Price + " kr."; }
        }


        public int TotalPages
        {
            get { return _totalPages; }
            private set { _totalPages = value; }
        }

        public int CurrentPages => _currentPage;

        #endregion

#region Commands
        ICommand _Button1Click;
        public ICommand Button1Command { get { return _Button1Click ?? (_Button1Click = new RelayCommand(Button1CommandExecute, Button1CommandCanExecute)); } }

        private void Button1CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[0].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[0].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[0], 1));
            }
        }

        bool Button1CommandCanExecute()
        {
            if (_products[0].Price == 0) return false;
            else return true;
        }

        ICommand _Button2Click;
        public ICommand Button2Command { get { return _Button2Click ?? (_Button2Click = new RelayCommand(Button2CommandExecute, Button2CommandCanExecute)); } }

        private void Button2CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[1].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[1].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[1], 1));
            }
        }

        bool Button2CommandCanExecute()
        {
            if (_products[1].Price == 0) return false;
            else return true;
        }

        ICommand _Button3Click;
        public ICommand Button3Command { get { return _Button3Click ?? (_Button3Click = new RelayCommand(Button3CommandExecute, Button3CommandCanExecute)); } }

        private void Button3CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[2].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[2].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[2], 1));
            }
        }

        bool Button3CommandCanExecute()
        {
            if (_products[2].Price == 0) return false;
            else return true;
        }

        ICommand _Button4Click;
        public ICommand Button4Command { get { return _Button4Click ?? (_Button4Click = new RelayCommand(Button4CommandExecute, Button4CommandCanExecute)); } }

        private void Button4CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[3].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[3].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[3], 1));
            }
        }

        bool Button4CommandCanExecute()
        {
            if (_products[3].Price == 0) return false;
            else return true;
        }

        ICommand _Button5Click;
        public ICommand Button5Command { get { return _Button5Click ?? (_Button5Click = new RelayCommand(Button5CommandExecute, Button5CommandCanExecute)); } }

        private void Button5CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[4].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[4].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[4], 1));
            }
        }

        bool Button5CommandCanExecute()
        {
            if (_products[4].Price == 0) return false;
            else return true;
        }

        ICommand _Button6Click;
        public ICommand Button6Command { get { return _Button6Click ?? (_Button6Click = new RelayCommand(Button6CommandExecute, Button6CommandCanExecute)); } }

        private void Button6CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[5].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[5].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[5], 1));
            }
        }

        bool Button6CommandCanExecute()
        {
            if (_products[5].Price == 0) return false;
            else return true;
        }

        ICommand _Button7Click;
        public ICommand Button7Command { get { return _Button7Click ?? (_Button7Click = new RelayCommand(Button7CommandExecute, Button7CommandCanExecute)); } }

        private void Button7CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[6].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[6].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[6], 1));
            }
        }

        bool Button7CommandCanExecute()
        {
            if (_products[6].Price == 0) return false;
            else return true;
        }

        ICommand _Button8Click;
        public ICommand Button8Command { get { return _Button8Click ?? (_Button8Click = new RelayCommand(Button8CommandExecute, Button8CommandCanExecute)); } }

        private void Button8CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[7].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[7].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[7], 1));
            }
        }

        bool Button8CommandCanExecute()
        {
            if (_products[7].Price == 0) return false;
            else return true;
        }

        ICommand _Button9Click;
        public ICommand Button9Command { get { return _Button9Click ?? (_Button9Click = new RelayCommand(Button9CommandExecute, Button9CommandCanExecute)); } }

        private void Button9CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[8].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[8].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[8], 1));
            }
        }

        bool Button9CommandCanExecute()
        {
            if (_products[8].Price == 0) return false;
            else return true;
        }

        ICommand _Button10Click;
        public ICommand Button10Command { get { return _Button10Click ?? (_Button10Click = new RelayCommand(Button10CommandExecute, Button10CommandCanExecute)); } }

        private void Button10CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[9].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[9].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[9], 1));
            }
        }

        bool Button10CommandCanExecute()
        {
            if (_products[9].Price == 0) return false;
            else return true;
        }

        ICommand _Button11Click;
        public ICommand Button11Command { get { return _Button11Click ?? (_Button11Click = new RelayCommand(Button11CommandExecute, Button11CommandCanExecute)); } }

        private void Button11CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[10].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[10].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[10], 1));
            }
        }

        bool Button11CommandCanExecute()
        {
            if (_products[10].Price == 0) return false;
            else return true;
        }

        ICommand _Button12Click;
        public ICommand Button12Command { get { return _Button12Click ?? (_Button12Click = new RelayCommand(Button12CommandExecute, Button12CommandCanExecute)); } }

        private void Button12CommandExecute()
        {
            if (_shopList.Any(x => x.Name == _products[11].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[11].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[11], 1));
            }
        }

        bool Button12CommandCanExecute()
        {
            if (_products[11].Price == 0) return false;
            else return true;
        }
        #endregion
    }
}
