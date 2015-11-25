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
    /// <summary>
    ///     En klasse der indeholder en liste med menuCategories samt funktionalitet til at arbejde med disse. Derved kan der vælges forskellige
    ///     kategorier af knapper og disse kan derefter afspejles i kasseapparatet.
    /// </summary>
    public class CategoriesMenu : ObservableCollection<MenuCategory>, INotifyPropertyChanged
    {
        private ProductCategoryList _productCategoryList;
        private Button CategoryButton;
        private List<Product> _totalList;

        /// <summary>
        ///     Constructor: Opretter variabler og sætter produktknapperne til at vise alle produkter uanset kategori som default.
        /// </summary>
        public CategoriesMenu()
        {
            _productCategoryList = new ProductCategoryList();
            CategoryButton = (Button)Application.Current.MainWindow.FindName("CategoryMenu");

            _totalList = CreateListOfAllProducts();

            Update();
        }

        /// <summary>
        ///     Opdaterer kategorierne udfra indholdet i ProduktCategoryList. Denne funktion kan kaldes hvis brugeren af kasseapparatet ønsker
        ///     at hente produkterne fra databasen.
        /// </summary>
        public void Update()
        {
            CategoryButton.ContextMenu.Items.Clear();
            _productCategoryList.Update();
            _totalList = CreateListOfAllProducts();
            var MenuCat = new MenuCategory("All products", _totalList);
            MenuCat.Command.Execute(this);

            CategoryButton.ContextMenu.Items.Add(MenuCat);

            //Create categories from categories list
            for (int i = 0; i < _productCategoryList.Count; i++)
            {
                var MenuCateg = new MenuCategory(_productCategoryList[i].Name, _productCategoryList[i].Products.ToList());

                CategoryButton.ContextMenu.Items.Add(MenuCateg);
            }
        }

        /// <summary>
        ///     Denne funktion tager produkterne fra alle kategorier og opretter en liste med alle produkterne. Derved kan en kategori oprettes der
        ///     viser alle produkter på 1 gang.
        /// </summary>
        /// <returns></returns>
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
