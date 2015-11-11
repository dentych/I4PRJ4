using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KasseApparat.ProductMenuControl;

namespace KasseApparat
{
    public class CategoriesMenu : ObservableCollection<MenuCategory>, INotifyPropertyChanged
    {
        private ProductCategoryList _productCategoryList;
        private Button CategoryButton;
        private ProductButtonControl _productButtonControl;

        public CategoriesMenu()
        {
            _productCategoryList = new ProductCategoryList();
            CategoryButton = (Button)Application.Current.MainWindow.FindName("CategoryMenu");
            _productButtonControl = (ProductButtonControl)Application.Current.MainWindow.FindResource("ProductButtonControl");

            Update();
        }

        public void Update()
        {

            foreach (var category in _productCategoryList)
            {
                var categoryItem = new MenuItem();
                categoryItem.Header = category.Name;
                categoryItem.MinHeight = 50;
                categoryItem.Click += CategoryItemOnClick;
                categoryItem.Background = new SolidColorBrush();

                CategoryButton.ContextMenu.Items.Add(categoryItem);
            }
        }

        private void CategoryItemOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            string pressedCategory = (sender as MenuItem).Header.ToString();
            CategoryButton.Content = pressedCategory;

            if (_productButtonControl != null)
            {
                foreach (var category in _productCategoryList)
                {
                    if (pressedCategory == category.Name)
                    {
                        _productButtonControl.Update(category.Products);
                    }
                }
            }
        }
    }
}
