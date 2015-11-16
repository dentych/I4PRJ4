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
using SharedLib.Models;

namespace KasseApparat
{
    public class CategoriesMenu : ObservableCollection<MenuCategory>, INotifyPropertyChanged
    {
        private ProductCategoryList _productCategoryList;
        private Button CategoryButton;
        private List<Product> _totalList;


        /* Ctor: Contructor. Opretter variable og updaterer kategorier 
        */
        public CategoriesMenu()
        {
            _productCategoryList = new ProductCategoryList();
            CategoryButton = (Button)Application.Current.MainWindow.FindName("CategoryMenu");

            _totalList = CreateListOfAllProducts();
            var MenuCat = new MenuCategory("All products", _totalList);
            MenuCat.Command.Execute(this);

            Update();
        }

        public void Update()
        {
            CategoryButton.ContextMenu.Items.Clear();
            _productCategoryList.Update();
            _totalList = CreateListOfAllProducts();
            var MenuCat = new MenuCategory("All products", _totalList);

            CategoryButton.ContextMenu.Items.Add(MenuCat);

            //Create categories from categories list
            for (int i = 0; i < _productCategoryList.Count; i++)
            {
                var MenuCateg = new MenuCategory(_productCategoryList[i].Name, _productCategoryList[i].Products.ToList());



                CategoryButton.ContextMenu.Items.Add(MenuCateg);
            }
        }

        private List<Product> CreateListOfAllProducts()
        {
            List<Product> totalProducts = new List<Product>();

            foreach (var Category in _productCategoryList)
            {
                totalProducts.AddRange(Category.Products);
            }

            return totalProducts;
        }
    }
}
