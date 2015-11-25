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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
            DataContext = new AddCategoryViewModel();
            FocusManager.SetFocusedElement(this, textboxName);
        }

        /* LUK LORTET */
        private void Annuller(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

