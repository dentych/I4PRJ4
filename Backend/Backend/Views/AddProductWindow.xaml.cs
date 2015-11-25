using System.Windows;
using System.Windows.Input;
using Backend.Models;
using Backend.ViewModels;

namespace Backend.Views
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
            FocusManager.SetFocusedElement(this, textboxName);

        }




        /* VALIDER */
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