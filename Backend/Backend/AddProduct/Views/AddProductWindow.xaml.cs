using System.Windows;
using System.Windows.Input;
using Backend.AddProduct.Brains;
using Backend.AddProduct.ViewModels;
using Backend.Communication;

namespace Backend
{
    /// <summary>
    ///     Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow
    {
    
        public AddProductWindow()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel();
        }















        /* Validate input */

        private void textboxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ",")
            {
                e.Handled = true;
            }
        }
        /* LUK LORTET */
        private void Annuller(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}