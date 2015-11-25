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
using System.Windows.Shapes;
using Backend.ViewModels;

namespace Backend.Views
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public EditProductWindow()
        {
            DataContext = new EditProductViewModel();
            InitializeComponent();
            FocusManager.SetFocusedElement(this, textboxName);
        }

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
