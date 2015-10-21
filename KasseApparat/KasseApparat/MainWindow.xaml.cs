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

#region Inputs
        private void ButtonCash_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shopList = (ShoppingList)this.FindResource("ShoppingList");
            if (Display.Text.ToString() != "")
                MessageBox.Show("Total pris: " + shopList.TotalPrice 
                    + "\nBetalt: " + Display.Text
                    + "\nDifference: " + (shopList.TotalPrice - Convert.ToDouble(Display.Text)));
        }

        private void ButtonNr_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            Display.Text += button.Content;
        }

        private void ButtonClr_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
        }

#endregion

        private void ButtonProductClick(object sender, RoutedEventArgs e)
        {
            Button buttonName = (Button)e.OriginalSource;
            ProductButtonControl ProdControl = (ProductButtonControl) this.FindResource("ProductButtonControl");
            
            ProdControl.addItem(int.Parse(buttonName.Tag.ToString()));
        }
    }
}
