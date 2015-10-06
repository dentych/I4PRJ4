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
        private List<Product> _products;
        private ShoppingList _shopList;
#endregion

        public ProductButtonControl()
        {
            _productList = new ProductList();
            _products = new List<Product>();
            _shopList = (ShoppingList)Application.Current.MainWindow.FindResource("ShoppingList");

            

            Update();
        }

        public void Update()
        {
            _productList.Update();
            SetButtons(1, PageChange.Currentpage);
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

        public void SetButtons(int page, PageChange Change)
        {
            int pageIndex;

            if (Change == PageChange.Nextpage)
            {
                pageIndex = (page)*12;
                _currentPage++;
            }
            else if (Change == PageChange.Currentpage)
            {
                pageIndex = (page - 1)*12;
            }
            else
            {

                pageIndex = (page - 2)*12;
                _currentPage--;
            }

            for (int i = pageIndex; i < pageIndex+12; i++)
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
        }

        public void addItem(int indexItem)
        {
            if (_shopList.Any(x => x.Name == _products[indexItem].Name))
            {
                var purchasedProduct = _shopList.Where(x => x.Name == _products[indexItem].Name).Single();
                int index = _shopList.IndexOf(purchasedProduct);
                _shopList.IncrementQuantity(index);
            }
            else
            {
                _shopList.AddItem(new PurchasedProduct(_products[indexItem], 1));
            }
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

        public int CurrentPages
        {
            get { return _currentPage; }
        }

        #endregion

    }
}
