using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharedLib.Models;

namespace KasseApparat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CategoriesMenu _catMenu;
        public MainWindow()
        {
            InitializeComponent();
            _catMenu = new CategoriesMenu();
        }

#region Inputs
        private void ButtonCash_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            if (Display.Text.ToString() != "")
            {
                shopList.AddItem(new PurchasedProduct(new Product()
                {
                    Name = "Kontant",
                    Price = -Convert.ToDecimal(Display.Text),
                }, 1, 1));
                Display.Text = "";
            }
            if (shopList.TotalPrice < 0)
            {
                MessageBox.Show("Retur: " + shopList.TotalPrice);
                RetBox.Content = shopList.TotalPrice;
                shopList.EndPurchase();
            }
        }

        private void ButtonQuant_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            if (shopList.Count > 0)
                shopList.SetQuantity(Convert.ToInt32(Display.Text));
            Display.Text = "";
        }

        private void ButtonNr_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            Display.Text += button.Content;
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            shopList.SetQuantity(-Convert.ToInt32(Display.Text));
            Display.Text = "";
        }

        private void ButtonClr_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            

            Display.Text = "";
        }

        private void CategoryItemOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
        }

        #endregion
        /*
        private void ButtonRefreshClick(object sender, RoutedEventArgs e)
        {
            ProductButtonControl ProdControl = (ProductButtonControl)this.FindResource("ProductButtonControl");

            ProdControl.Update();
        }
        */

        private void Categori_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;

            _catMenu.Update();
        }
    }
}
