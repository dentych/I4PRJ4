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
            //var productButton = new ProductButtonControl();
            
        }

        private void ButtonCash_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            MessageBox.Show("Total pris: " + shopList.TotalPrice + "\nPaid " + Display.Text);
        }

        private void ButtonOneDZero_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonProductClick(object sender, RoutedEventArgs e)
        {
            Button buttonName = (Button)e.OriginalSource;
            ProductButtonControl ProdControl = (ProductButtonControl) this.FindResource("ProductButtonControl");
            string a = buttonName.Name;
            string b = string.Empty;

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            ProdControl.addItem(int.Parse(b)-1);
        }
    }
}
