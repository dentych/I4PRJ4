using System.Windows;
using System.Windows.Input;
using Backend.AddProduct;
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
        private IAddProduct backend;

        public AddProductWindow()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel();
            backend = new AddProductCB(new PrjProtokol(), new Client("127.0.0.1", 9000));
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