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
            ShoppingList ShopList = (ShoppingList)this.FindResource("ShoppingList");
            int totPrice = ShopList.TotalWarePrice();
            MessageBox.Show("Total pris: " + totPrice);
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
            
        }

        private void ButtonLessClick(object sender, RoutedEventArgs e)
        {
            var 
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
