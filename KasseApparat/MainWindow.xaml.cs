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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KasseApparat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void ButtonCash_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            MessageBox.Show("Total pris: " + shopList.TotalPrice);
        }

        private void ButtonOneDZero_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonUpClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonDownClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonMoreClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxShoppingList.SelectedIndex != -1)
            {
                ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
                shopList[ListBoxShoppingList.SelectedIndex].Quantity++;
                ListBoxShoppingList.Items.Refresh();

                //Så den ikke inkrementerer for hurtigt
                System.Threading.Thread.Sleep(100);
            }
        }

        private void ButtonLessClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxShoppingList.SelectedIndex == -1) return; 
                var shopList = (ShoppingList) this.FindResource("ShoppingList");
                var index = ListBoxShoppingList.SelectedIndex;

                if (shopList[index].Quantity-1 == 0)
                {
                    shopList.RemoveAt(index);
                    return;
                }

                shopList[index].Quantity--;
                ListBoxShoppingList.Items.Refresh();

                //Så den ikke dekrementerer for hurtigt
                System.Threading.Thread.Sleep(100);
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
